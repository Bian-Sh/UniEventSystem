using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using zFrame.Event;
/// <summary>
/// 颜色需求事件参数类
/// </summary>
public class ColorEventArgs : BaseEventArgs
{
    /// <summary>
    /// 指定的颜色 
    /// </summary>
    public Color Color { private set; get; }
    /// <summary>
    /// 鼠标或者触笔事件
    /// </summary>
    /// <param name="_t">事件类型</param>
    /// <param name="_sender">事件发送者</param>
    /// <param name="_selected">被选中的游戏对象</param>
    /// <param name="_buttonID">按键编号</param>
    /// <param name="_hit">碰撞点信息</param>
    public ColorEventArgs Config(ColorEvent _t, GameObject _sender, Color color)
    {
        base.Config(_t, _sender);
        this.Color = color;
        return this;
    }

}

/// <summary>
/// Just For Test 
/// </summary>
public enum ColorEvent
{
    /// <summary>
    /// 使用指定颜色改变对象颜色
    /// </summary>
    ChangeTo,

}
