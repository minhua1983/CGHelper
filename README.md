# CGHelper
这是一个某款过气网游的脚本

## 功能说明
* 可以自动鉴定鱼，但是没有魔法后得手动补魔，靠这个把鉴定升级到满级了。
* 可以自动走路遇敌。遇敌后得自己手动操作，一般用来抓宠时自动走路。

## 其他说明
* 用windows底层api来获取当前游戏的句柄，这个是最基本的。
* 用windows底层api来模拟鼠标和键盘，其他游戏的脚本基本也是靠这个来模拟键鼠操作的，这个也是基础。
* 用windows底层api的hook技术，来捕捉键盘按钮是否被按，用于开启或关闭自动走路，lol中快捷键的设置，我猜也是用这类技术来处理的。

## 免责声明
本脚本代码仅供学习，请不要用于商业，也不要用于任何违法行为。任何使用者如果对游戏公司造成了商业利益上或名誉上的侵害，本人将不承担任何责任，一切责任将由使用者承担。如果不同意此免责声明中所申明的内容，请勿下载本源码；一旦下载本源码则视为同意本免责声明。再次声明：请勿用于商业，也不要用于任何违法行为。
