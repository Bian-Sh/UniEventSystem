using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using zFrame.Event;
/// <summary>
/// 鼠标事件参数类
/// </summary>
public class StylusEventArgs : BaseEventArgs
{
    /// <summary>
    /// 光标下的游戏对象
    /// </summary>
    public GameObject Selected { private set; get; }
    /// <summary>
    /// 光标与游戏对象的触碰点
    /// </summary>
    public RaycastHit HitInfo { private set; get; } = default(RaycastHit);
    /// <summary>
    /// 鼠标或触笔的按键ID
    /// </summary>
    public int ButtonID { private set; get; }
    /// <summary>
    /// 鼠标或者触笔事件
    /// </summary>
    /// <param name="_t">事件类型</param>
    /// <param name="_sender">事件发送者</param>
    /// <param name="_selected">被选中的游戏对象</param>
    /// <param name="_buttonID">按键编号</param>
    /// <param name="_hit">碰撞点信息</param>
    public StylusEventArgs Config(StylusEvent _t, GameObject _sender, GameObject _selected, int _buttonID = -1, RaycastHit _hit = default(RaycastHit))
    {
        base.Config(_t, _sender);
        Selected = _selected;
        ButtonID = _buttonID;
        HitInfo = _hit;
        return this;

    }

    public override void Dispose()
    {
        base.Dispose();
        Selected = null;
        ButtonID = -1;
        HitInfo = default(RaycastHit);
    }
}