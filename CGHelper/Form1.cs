using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace CGHelper
{
    public partial class Form1 : Form
    {
        #region 鼠标穿透(暂时没用到)
        private const uint WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int GWL_STYLE = (-16);
        private const int GWL_EXSTYLE = (-20);
        private const int LWA_ALPHA = 0x2;

        [DllImport("user32", EntryPoint = "SetWindowLong")]
        private static extern uint SetWindowLong(
        IntPtr hwnd,
        int nIndex,
        uint dwNewLong
        );

        [DllImport("user32", EntryPoint = "GetWindowLong")]
        private static extern uint GetWindowLong(
        IntPtr hwnd,
        int nIndex
        );

        [DllImport("user32", EntryPoint = "SetLayeredWindowAttributes")]
        private static extern int SetLayeredWindowAttributes(
        IntPtr hwnd,
        int crKey,
        int bAlpha,
        int dwFlags
        );

        /// <summary>
        /// 使窗口有鼠标穿透功能
        /// </summary>
        public void CanPenetrate()
        {
            uint intExTemp = GetWindowLong(this.Handle, GWL_EXSTYLE);
            uint oldGWLEx = SetWindowLong(this.Handle, GWL_EXSTYLE, WS_EX_TRANSPARENT | WS_EX_LAYERED);
            //SetLayeredWindowAttributes(this.Handle, 0, 200, LWA_ALPHA);
        }
        #endregion

        #region 导入鼠标事件(目前已经废弃，因为它是真正控制鼠标，不是后台控制)
        const int MOUSEEVENTF_MOVE = 0x0001;      //移动鼠标 
        const int MOUSEEVENTF_LEFTDOWN = 0x0002; //模拟鼠标左键按下 
        const int MOUSEEVENTF_LEFTUP = 0x0004; //模拟鼠标左键抬起 
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008; //模拟鼠标右键按下 
        const int MOUSEEVENTF_RIGHTUP = 0x0010; //模拟鼠标右键抬起 
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020; //模拟鼠标中键按下 
        const int MOUSEEVENTF_MIDDLEUP = 0x0040; //模拟鼠标中键抬起 
        const int MOUSEEVENTF_ABSOLUTE = 0x8000; //标示是否采用绝对坐标 

        [DllImport("user32")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        #endregion

        #region 通过PostMessage后台模拟鼠标移动，点击
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("user32.dll", EntryPoint = "PostMessage", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hwnd, int msg, int wParam, int lParam);
        #endregion

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left; //最左坐标
            public int Top; //最上坐标
            public int Right; //最右坐标
            public int Bottom; //最下坐标
        }



        Thread t;
        CGDirection direction = CGDirection.EW;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //* 通过System.Windows.Forms.Timer可以监测全局鼠标坐标
            System.Windows.Forms.Timer T = new System.Windows.Forms.Timer();
            T.Interval = 40;
            T.Tick += new EventHandler(T_Tick);
            T.Enabled = true;
            //*/

            /* 鼠标穿透
            AllowTransparency = true;
            this.Opacity = 0.5;
            TopMost = true;
            //BackColor = Color.Blue;
            CanPenetrate();
            //*/
        }

        void T_Tick(object sender, EventArgs e)
        {
            Point srceenPoint = Control.MousePosition;
            xLabel.Text = srceenPoint.X.ToString();
            yLabel.Text = srceenPoint.Y.ToString();
            int x = srceenPoint.X;
            int y = srceenPoint.Y;
            x = 600;
            y = 30;
            //236,217,182
            colorlabel.Text = GetPixelColor(x, y).R.ToString();
            if (colorlabel.Text == "236")
            {
                statuslabel.Text = "移动";
            }
            else
            {
                statuslabel.Text = "战斗";
                if (t != null)
                {
                    t.Abort();
                    t = null;
                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //此鼠标监测，只能作用在当前窗体内。
            //Point srceenPoint = Control.MousePosition;
            //xLabel.Text = srceenPoint.X.ToString();
            //yLabel.Text = srceenPoint.Y.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (t == null)
            {
                t = new Thread(Loop);
                t.Start();

                #region 启用HOOK
                if (m_HookHandle == 0)
                {
                    m_KbdHookProc = new HookProc(KeyboardHookProc);
                    //当前窗体HOOK
                    //m_HookHandle = SetWindowsHookEx(WH_KEYBOARD, m_KbdHookProc, IntPtr.Zero, GetCurrentThreadId());
                    //全局HOOK
                    m_HookHandle = SetWindowsHookEx(13, m_KbdHookProc, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
                    //m_HookHandle = SetWindowsHookEx(13, m_KbdHookProc, GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), 0);

                    //VS调试时，一直返回0
                    //1). 在 Visual Studio 中打开项目。
                    //2). 在“项目”菜单上单击“属性”。
                    //3). 单击“调试”选项卡。
                    //4). 清除“启用 Visual Studio 宿主进程”复选框。
                    if (m_HookHandle == 0)
                    {
                        MessageBox.Show("呼叫 SetWindowsHookEx 失败!");
                        return;
                    }
                }
                #endregion
            }
        }

        void Loop()
        {
            int baseTime = 500;
            baseTime = baseTime + GetRandomNumber().Next(0, Convert.ToInt32(r0textBox.Text));

            int x0, y0, x1, y1;

            if (direction == CGDirection.EW)
            {
                x0 = Convert.ToInt32(x0textBox.Text) + GetRandomNumber().Next(-30, 30);
                y0 = Convert.ToInt32(y0textBox.Text) + GetRandomNumber().Next(-23 * 3, 23 * 2);
                x1 = Convert.ToInt32(x1textBox.Text) + GetRandomNumber().Next(-30, 30);
                y1 = Convert.ToInt32(y1textBox.Text) + GetRandomNumber().Next(-23 * 3, 23 * 2);
            }
            else
            {
                x0 = Convert.ToInt32(x0textBox.Text) + GetRandomNumber().Next(-30 * 2, 30 * 3);
                y0 = Convert.ToInt32(y1textBox.Text) + GetRandomNumber().Next(-23, 23);
                x1 = Convert.ToInt32(x1textBox.Text) + GetRandomNumber().Next(-30 * 2, 30 * 3);
                y1 = Convert.ToInt32(y0textBox.Text) + GetRandomNumber().Next(-23, 23);
            }

            Move(x0, y0);

            Thread.Sleep(baseTime);
            Application.DoEvents();
            Move(x1, y1);
            Thread.Sleep(baseTime);
            Application.DoEvents();
            Loop();
        }

        Random GetRandomNumber()
        {
            Random r = new Random();
            return r;
        }

        public delegate void UpdateListBoxHandler(IntPtr WINDOW_HANDLER, int x, int y);

        void UpdateListBox(IntPtr WINDOW_HANDLER, int x, int y)
        {
            //MessageBox.Show(WINDOW_HANDLER.ToString() + ":" + x.ToString() + ":" + y.ToString());
            //MessageBox.Show(intPtrListBox.Items.Count.ToString());
            if (intPtrListBox.Items.Count <= 0)
            {
                intPtrListBox.Items.Add(WINDOW_HANDLER.ToString());
                intPtrListBox.SelectedItem = WINDOW_HANDLER.ToString();
            }
            else
            {
                if (!intPtrListBox.Items.Contains(WINDOW_HANDLER.ToString()))
                {
                    intPtrListBox.Items.Add(WINDOW_HANDLER.ToString());
                }
            }

            if (WINDOW_HANDLER.ToString() == intPtrListBox.SelectedItem.ToString())
            {
                /*
                //需要administrator权限来跑这个程序
                //此处通过spy++获取所有句柄
                IntPtr EDIT_WINDOW_HANDLER = FindWindowEx(WINDOW_HANDLER, IntPtr.Zero, "Edit", "");
                //MessageBox.Show(WINDOW_HANDLER.ToString());
                //MessageBox.Show(this.maskedTextBox1.Handle.ToString());
                //MessageBox.Show(EDIT_WINDOW_HANDLER.ToString());
                //int i = SendMessage(this.maskedTextBox1.Handle, 0x0102, 65, 0);
                //int i = SendMessage(WINDOW_HANDLER, 0x0102, 65, 0);
                //bool i = PostMessage(EDIT_WINDOW_HANDLER, 0x0102, 65, 0);
                //MessageBox.Show(i.ToString());
                PostMessage(EDIT_WINDOW_HANDLER, 0x0201, 0, MAKELONG(40,40));
                PostMessage(EDIT_WINDOW_HANDLER, 0x0202, 0, MAKELONG(40, 40));
                //*/
                //MessageBox.Show(WINDOW_HANDLER.ToString());

                PostMessage(WINDOW_HANDLER, 0x0200, 1, MAKELONG(y, x));
                PostMessage(WINDOW_HANDLER, 0x0201, 1, MAKELONG(y, x));
                PostMessage(WINDOW_HANDLER, 0x0202, 1, MAKELONG(y, x));
                //MessageBox.Show(GetLastError().ToString());
            }
        }

        void Move(int x, int y)
        {
            IntPtr handler = IntPtr.Zero;
            do
            {
                handler = FindWindowEx(IntPtr.Zero, handler, "魔力宝贝", intPtrTextBox.Text);
                //MessageBox.Show(handler.ToString());
                if (handler!=IntPtr.Zero)
                {
                    UpdateListBoxHandler updateListBoxHandler = new UpdateListBoxHandler(UpdateListBox);
                    this.Invoke(updateListBoxHandler, handler, x, y);
                    //this.Invoke(new UpdateListBoxHandler((WINDOW_HANDLER, intX, intY) => {/*逻辑//*/ }), handler, x, y);
                }
            }
            while (handler != IntPtr.Zero);
        }

        int MAKELONG(int x, int y)
        {
            return ((((int)x) << 16) | y); //low order WORD 是指标的x位置； high order WORD是y位置.
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (t != null)
            {
                t.Abort();
                t = null;

                #region 销毁HOOK
                if (m_HookHandle != 0)
                {
                    bool ret = UnhookWindowsHookEx(m_HookHandle);
                    if (ret == false)
                    {
                        MessageBox.Show("呼叫 UnhookWindowsHookEx 失败!");
                        return;
                    }
                    m_HookHandle = 0;
                }
                #endregion
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (t != null)
            {
                t.Abort();
                t = null;

                #region 销毁HOOK
                if (m_HookHandle != 0)
                {
                    bool ret = UnhookWindowsHookEx(m_HookHandle);
                    if (ret == false)
                    {
                        MessageBox.Show("呼叫 UnhookWindowsHookEx 失败!");
                        return;
                    }
                    m_HookHandle = 0;
                }
                #endregion
            }
        }

        #region 覆盖基类ProcessCmdKey以实现当前窗口键盘事件捕捉
        /*
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //此从写的方法，仅限于当前窗口捕捉键盘，效果和局部HOOK一样。

            switch (keyData)
            {
                case Keys.Escape:
                    MessageBox.Show("ESC");
                    break;
                default:
                    MessageBox.Show("ESC");
                    break;
            }



            //return false;//如果要调用KeyDown,这里一定要返回false才行,否则只响应重写方法里的按键.
            //这里调用一下父类方向,相当于调用普通的KeyDown事件.//所以按空格会弹出两个对话框
            //return base.ProcessCmdKey(ref msg, keyData);
            return true;//这里return true 否则控件焦点会跟着方向键改变
        }
        //*/
        #endregion

        #region 导入键盘事件
        [DllImport("user32.dll", EntryPoint = "keybd_event")]

        public static extern void keybd_event(
        byte bVk, //虚拟键值  
        byte bScan,// 一般为0  
        int dwFlags, //这里是整数类型 0 为按下，2为释放  
        int dwExtraInfo //这里是整数类型 一般情况下设成为0  
        );
        #endregion


        private void button3_Click(object sender, EventArgs e)
        {
            CheckImage();
        }

        void CheckImage()
        {

            //激活CG
            Move(250, 333);
            Thread.Sleep(1000);
            Application.DoEvents();
            //打开人物属性
            //keybd_event(65, 0, 0, 0);
            //keybd_event(66, 0, 0, 0);
            //keybd_event(66, 0, 2, 0);
            //keybd_event(65, 0, 2, 0);
            //Thread.Sleep(500);
            keybd_event(17, 0, 0, 0);
            keybd_event(81, 0, 0, 0);
            Thread.Sleep(1000);
            Application.DoEvents();
            keybd_event(81, 0, 2, 0);
            keybd_event(17, 0, 2, 0);
            Thread.Sleep(1000);
            Application.DoEvents();


            //Image objImage = new Bitmap(72, 36);
            Image objImage = new Bitmap(60, 12);
            //Image objImage = new Bitmap(72, 36);
            Graphics g = Graphics.FromImage(objImage);
            //g.CopyFromScreen(new Point(0, 0), new Point(-1358, -862), new Size(1440, 900));
            //g.CopyFromScreen(new Point(0, 0), new Point(-21, -42), new Size(1920, 1080));
            g.CopyFromScreen(new Point(0, 0), new Point(-76, -263), new Size(1920, 1080));
            //g.CopyFromScreen(new Point(0, 0), new Point(-1830, -1042), new Size(1920, 1080));
            IntPtr dc1 = g.GetHdc();
            g.ReleaseHdc(dc1);

            objImage.Save(AppDomain.CurrentDomain.BaseDirectory + "snap.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);


            //*
            //*4倍显示更精确
            Double times = 4;
            try
            {
                times = Convert.ToDouble(textBox1.Text);
            }
            catch (Exception)
            {

            }
            //Image temp = new Bitmap(objImage, 256, 144);
            Image temp = new Bitmap(objImage, Convert.ToInt32(60 * times), Convert.ToInt32(12 * times));
            //Image temp = new Bitmap(objImage, 72*times, 36*times);
            string file = AppDomain.CurrentDomain.BaseDirectory + "snap.max.jpg";
            temp.Save(file);
            //*/
            //string file = AppDomain.CurrentDomain.BaseDirectory + "snap.jpg";
            //*/
            //string file = AppDomain.CurrentDomain.BaseDirectory + "snap.jpg";
            SetImage(file);
            string hp = Analyse();
            //MessageBox.Show(hp);
            hplabel.Text = hp;

            keybd_event(17, 0, 0, 0);
            keybd_event(81, 0, 0, 0);
            Thread.Sleep(1000);
            Application.DoEvents();
            keybd_event(81, 0, 2, 0);
            keybd_event(17, 0, 2, 0);
            //Thread.Sleep(1000);

            //CheckImage();



        }

        #region Stream和byte[]相互转换
        /*
        /// 将 Stream 转成 byte[]

        public byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// 将 byte[] 转成 Stream

        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
        //*/
        #endregion

        #region MODI识别器
        public MODI.Document _MODIDocument;
        private void SetImage(string filename)
        {
            // Bitmap nump = new Bitmap();
            // Bitmap myBitmap1 = new Bitmap(new fa;
            // set the image..
            try
            {
                _MODIDocument = new MODI.Document();
                _MODIDocument.Create(filename);
                //axMiDocView1.Document = _MODIDocument;
                //axMiDocView1.Refresh();
            }
            catch (System.Runtime.InteropServices.COMException ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        public string Analyse()
        {
            string texts = "";
            if (_MODIDocument == null) return texts;
            try
            {
                // add event handler for progress visualisation
                //_MODIDocument.OnOCRProgress += new MODI._IDocumentEvents_OnOCRProgressEventHandler(this.ShowProgress);
                // the MODI call for OCR
                // _MODIDocument.OCR(_MODIParameters.Language,_MODIParameters.WithAutoRotation,_MODIParameters.WithStraightenImage);
                _MODIDocument.OCR(MODI.MiLANGUAGES.miLANG_ENGLISH, true, true);
                //this.textBox1.Text = (_MODIDocument.Images[0] as MODI.Image).Layout.Text;
                //MessageBox.Show((_MODIDocument.Images[0] as MODI.Image).Layout.Text);
                texts = (_MODIDocument.Images[0] as MODI.Image).Layout.Text;
                //statusBar1.Text = "Ready.";
                _MODIDocument.Close(false);
                _MODIDocument = null;

            }
            catch (Exception ee)
            {

                // simple exception "handling"
                //MessageBox.Show(@""+ee.Message+@""+ee.ToString()+@"");
            }
            return texts;
        }
        #endregion

        #region 导入HOOK
        const int WH_KEYBOARD = 2;

        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static int m_HookHandle = 0;    // Hook handle
        private HookProc m_KbdHookProc;            // 鍵盤掛鉤函式指標

        // 設置掛鉤.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
         CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn,
       IntPtr hInstance, int threadId);

        // 將之前設置的掛鉤移除。記得在應用程式結束前呼叫此函式.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
         CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        // 呼叫下一個掛鉤處理常式（若不這麼做，會令其他掛鉤處理常式失效）.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
       CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode,
        IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        static extern int GetCurrentThreadId();

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string name);

        public int KeyboardHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // 當按鍵按下及鬆開時都會觸發此函式，這裡只處理鍵盤按下的情形。
            bool isPressed = (lParam.ToInt32() & 0x80000000) == 0;

            if (nCode < 0 || !isPressed)
            {
                return CallNextHookEx(m_HookHandle, nCode, wParam, lParam);
            }

            // 取得欲攔截之按鍵狀態
            KeyStateInfo escKey = KeyboardInfo.GetKeyState(Keys.Escape);
            KeyStateInfo waveKey = KeyboardInfo.GetKeyState(Keys.Oem3);
            /*
            KeyStateInfo altKey = KeyboardInfo.GetKeyState(Keys.Alt);
            if (altKey.IsPressed)
            {
                System.Diagnostics.Debug.WriteLine("Alt Pressed!");
            }
            //*/

            if (escKey.IsPressed || waveKey.IsPressed)
            {
                if (escKey.IsPressed)
                {
                    direction = CGDirection.EW;
                }

                if (waveKey.IsPressed)
                {
                    direction = CGDirection.SN;
                }

                //MessageBox.Show("escKey!");
                if (t != null)
                {
                    t.Abort();
                    t = null;
                }
                else
                {
                    t = new Thread(Loop);
                    t.Start();
                }
            }

            return CallNextHookEx(m_HookHandle, nCode, wParam, lParam);
        }
        #endregion

        #region 导入GUI坐标颜色
        [DllImport("user32.dll", EntryPoint = "GetDC")]
        static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll", EntryPoint = "GetPixel")]
        static extern uint GetPixel(IntPtr hWnd, int XPos, int YPos);

        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

        public System.Drawing.Color GetPixelColor(int x, int y)
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            uint pixel = GetPixel(hdc, x, y);
            ReleaseDC(IntPtr.Zero, hdc);
            Color color = Color.FromArgb((int)(pixel & 0x000000FF),
            (int)(pixel & 0x0000FF00) >> 8,
            (int)(pixel & 0x00FF0000) >> 16);
            return color;
        }
        #endregion

        private void button4_Click(object sender, EventArgs e)
        {
            intPtrListBox.Items.Clear();
        }
    }

    public enum CGDirection
    {
        /// <summary>
        /// EastWest
        /// </summary>
        EW,
        /// <summary>
        /// SouthNorth
        /// </summary>
        SN
    }

    #region 键盘处理类(对于ALT特殊处理)
    public class KeyboardInfo
    {
        private KeyboardInfo() { }

        [DllImport("user32")]
        private static extern short GetKeyState(int vKey);

        public static KeyStateInfo GetKeyState(Keys key)
        {
            int vkey = (int)key;

            //Alt需要特殊处理
            if (key == Keys.Alt)
            {
                vkey = 0x12;    // VK_ALT
            }

            short keyState = GetKeyState(vkey);
            int low = Low(keyState);
            int high = High(keyState);
            bool toggled = (low == 1);
            bool pressed = (high == 1);

            return new KeyStateInfo(key, pressed, toggled);
        }

        private static int High(int keyState)
        {
            if (keyState > 0)
            {
                return keyState >> 0x10;
            }
            else
            {
                return (keyState >> 0x10) & 0x1;
            }

        }

        private static int Low(int keyState)
        {
            return keyState & 0xffff;
        }
    }
    #endregion

    #region 按键状态结构
    public struct KeyStateInfo
    {
        Keys m_Key;
        bool m_IsPressed;
        bool m_IsToggled;

        public KeyStateInfo(Keys key, bool ispressed, bool istoggled)
        {
            m_Key = key;
            m_IsPressed = ispressed;
            m_IsToggled = istoggled;
        }

        public static KeyStateInfo Default
        {
            get
            {
                return new KeyStateInfo(Keys.None, false, false);
            }
        }

        public Keys Key
        {
            get { return m_Key; }
        }

        public bool IsPressed
        {
            get { return m_IsPressed; }
        }

        public bool IsToggled
        {
            get { return m_IsToggled; }
        }
    }
    #endregion
}
