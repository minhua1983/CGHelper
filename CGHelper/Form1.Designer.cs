namespace CGHelper
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.xLabel = new System.Windows.Forms.Label();
            this.yLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.y0textBox = new System.Windows.Forms.TextBox();
            this.x1textBox = new System.Windows.Forms.TextBox();
            this.y1textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.r0textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.x0textBox = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.hplabel = new System.Windows.Forms.Label();
            this.colorlabel = new System.Windows.Forms.Label();
            this.statuslabel = new System.Windows.Forms.Label();
            this.intPtrLabel = new System.Windows.Forms.Label();
            this.intPtrTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.intPtrListBox = new System.Windows.Forms.ListBox();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // xLabel
            // 
            this.xLabel.AutoSize = true;
            this.xLabel.Location = new System.Drawing.Point(12, 200);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(11, 12);
            this.xLabel.TabIndex = 0;
            this.xLabel.Text = "x";
            // 
            // yLabel
            // 
            this.yLabel.AutoSize = true;
            this.yLabel.Location = new System.Drawing.Point(69, 200);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(11, 12);
            this.yLabel.TabIndex = 1;
            this.yLabel.Text = "y";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 111);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 21);
            this.button1.TabIndex = 2;
            this.button1.Text = "执行";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(14, 138);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 21);
            this.button2.TabIndex = 3;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(14, 165);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 21);
            this.button3.TabIndex = 4;
            this.button3.Text = "截图";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // y0textBox
            // 
            this.y0textBox.Location = new System.Drawing.Point(103, 9);
            this.y0textBox.Name = "y0textBox";
            this.y0textBox.Size = new System.Drawing.Size(50, 21);
            this.y0textBox.TabIndex = 6;
            this.y0textBox.Text = "144";
            // 
            // x1textBox
            // 
            this.x1textBox.Location = new System.Drawing.Point(47, 45);
            this.x1textBox.Name = "x1textBox";
            this.x1textBox.Size = new System.Drawing.Size(50, 21);
            this.x1textBox.TabIndex = 7;
            this.x1textBox.Text = "450";
            // 
            // y1textBox
            // 
            this.y1textBox.Location = new System.Drawing.Point(103, 45);
            this.y1textBox.Name = "y1textBox";
            this.y1textBox.Size = new System.Drawing.Size(50, 21);
            this.y1textBox.TabIndex = 8;
            this.y1textBox.Text = "340";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "起点";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "终点";
            // 
            // r0textBox
            // 
            this.r0textBox.Location = new System.Drawing.Point(222, 9);
            this.r0textBox.Name = "r0textBox";
            this.r0textBox.Size = new System.Drawing.Size(50, 21);
            this.r0textBox.TabIndex = 11;
            this.r0textBox.Text = "500";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(175, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "间隔";
            // 
            // x0textBox
            // 
            this.x0textBox.Location = new System.Drawing.Point(47, 9);
            this.x0textBox.Name = "x0textBox";
            this.x0textBox.Size = new System.Drawing.Size(50, 21);
            this.x0textBox.TabIndex = 5;
            this.x0textBox.Text = "196";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(222, 45);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(50, 21);
            this.textBox1.TabIndex = 13;
            this.textBox1.Text = "8.1";
            // 
            // hplabel
            // 
            this.hplabel.AutoSize = true;
            this.hplabel.Location = new System.Drawing.Point(117, 200);
            this.hplabel.Name = "hplabel";
            this.hplabel.Size = new System.Drawing.Size(11, 12);
            this.hplabel.TabIndex = 14;
            this.hplabel.Text = "0";
            // 
            // colorlabel
            // 
            this.colorlabel.AutoSize = true;
            this.colorlabel.Location = new System.Drawing.Point(12, 221);
            this.colorlabel.Name = "colorlabel";
            this.colorlabel.Size = new System.Drawing.Size(35, 12);
            this.colorlabel.TabIndex = 15;
            this.colorlabel.Text = "color";
            // 
            // statuslabel
            // 
            this.statuslabel.AutoSize = true;
            this.statuslabel.Location = new System.Drawing.Point(119, 221);
            this.statuslabel.Name = "statuslabel";
            this.statuslabel.Size = new System.Drawing.Size(29, 12);
            this.statuslabel.TabIndex = 16;
            this.statuslabel.Text = "移动";
            // 
            // intPtrLabel
            // 
            this.intPtrLabel.AutoSize = true;
            this.intPtrLabel.Location = new System.Drawing.Point(12, 84);
            this.intPtrLabel.Name = "intPtrLabel";
            this.intPtrLabel.Size = new System.Drawing.Size(29, 12);
            this.intPtrLabel.TabIndex = 17;
            this.intPtrLabel.Text = "句柄";
            // 
            // intPtrTextBox
            // 
            this.intPtrTextBox.Location = new System.Drawing.Point(47, 81);
            this.intPtrTextBox.Name = "intPtrTextBox";
            this.intPtrTextBox.Size = new System.Drawing.Size(213, 21);
            this.intPtrTextBox.TabIndex = 18;
            this.intPtrTextBox.Text = "魔力宝贝 3.0.0 [牧羊座 I]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(175, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "放大";
            // 
            // intPtrListBox
            // 
            this.intPtrListBox.FormattingEnabled = true;
            this.intPtrListBox.ItemHeight = 12;
            this.intPtrListBox.Location = new System.Drawing.Point(119, 111);
            this.intPtrListBox.Name = "intPtrListBox";
            this.intPtrListBox.Size = new System.Drawing.Size(120, 40);
            this.intPtrListBox.TabIndex = 20;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(119, 163);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 21;
            this.button4.Text = "清空";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 242);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.intPtrListBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.intPtrTextBox);
            this.Controls.Add(this.intPtrLabel);
            this.Controls.Add(this.statuslabel);
            this.Controls.Add(this.colorlabel);
            this.Controls.Add(this.hplabel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.r0textBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.y1textBox);
            this.Controls.Add(this.x1textBox);
            this.Controls.Add(this.y0textBox);
            this.Controls.Add(this.x0textBox);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.yLabel);
            this.Controls.Add(this.xLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label xLabel;
        private System.Windows.Forms.Label yLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox y0textBox;
        private System.Windows.Forms.TextBox x1textBox;
        private System.Windows.Forms.TextBox y1textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox r0textBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox x0textBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label hplabel;
        private System.Windows.Forms.Label colorlabel;
        private System.Windows.Forms.Label statuslabel;
        private System.Windows.Forms.Label intPtrLabel;
        private System.Windows.Forms.TextBox intPtrTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox intPtrListBox;
        private System.Windows.Forms.Button button4;
    }
}

