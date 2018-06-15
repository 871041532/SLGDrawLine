# SLGDrawLine
SLG手游画行军线

介绍：
1.画线模块，可用于SLG手游行军线。
2.多个不同速度、颜色、长度的线在一个DrawCall中实现。
3.材质中ShowLength控制两端显示的长度，AlphaLength控制渐隐长度，AllShowLength是一个临界值，长度小于它的线全部显示，长度大于它的线出现中间渐隐消失效果。（ShowLength、AlphaLength控制这两个效果）

使用demo：
1.画线：int handle=LineManager.Instance.DrawLine(Vector3 start,Vector3 end,Color color,float speed);
2.回收线：LineManager.Instance.BackLine(int handle);

效果：
![img]https://github.com/871041532/SLGDrawLine/blob/master/Assets/%E6%95%88%E6%9E%9C.gif
