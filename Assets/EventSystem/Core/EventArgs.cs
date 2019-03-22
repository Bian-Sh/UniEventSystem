using System;
using UnityEngine;
namespace zFrame.Event
{
    /// <summary>
    /// 事件信息类基类
    /// </summary>
    public abstract class BaseEventArgs
    {
        public readonly Enum m_Type;
        public readonly GameObject sender;
        public BaseEventArgs(Enum _t, GameObject _sender)
        {
            m_Type = _t;
            sender = _sender;
        }
    }
    /// <summary>
    /// UI事件参数类
    /// </summary>
    public class UIEventArgs : BaseEventArgs
    {
        /// <summary>
        /// 自定义标识符
        /// </summary>
        public readonly string flag;
        /// <summary>
        /// PopupUI需要用到的数据
        /// </summary>
        public readonly object msg;
        /// <summary>
        /// UI事件参数类
        /// </summary>
        /// <param name="_t">事件类型</param>
        /// <param name="_sender">事件发送者</param>
        /// <param name="_flag">自定义标识符</param>
        /// <param name="parms">传递的事件信息</param>
        public UIEventArgs(UIEvent _t, GameObject _sender, object parms, string _flag = "") : base(_t, _sender)
        {
            flag = string.IsNullOrEmpty(_flag) ? GetHashCode().ToString() : _flag;
            msg = parms;
        }
    }
    /// <summary>
    /// 鼠标事件参数类
    /// </summary>
    public class StylusEventArgs : BaseEventArgs
    {
        /// <summary>
        /// 光标下的游戏对象
        /// </summary>
        public readonly GameObject selected;
        /// <summary>
        /// 光标与游戏对象的触碰点
        /// </summary>
        public readonly RaycastHit hit = default(RaycastHit);
        /// <summary>
        /// 鼠标或触笔的按键ID
        /// </summary>
        public readonly int buttonID;
        /// <summary>
        /// 鼠标或者触笔事件
        /// </summary>
        /// <param name="_t">事件类型</param>
        /// <param name="_sender">事件发送者</param>
        /// <param name="_selected"></param>
        /// <param name="_position"></param>
        /// <param name="_buttonID"></param>
        public StylusEventArgs(StylusEvent _t, GameObject _sender, GameObject _selected, int _buttonID = -1, RaycastHit _hit = default(RaycastHit)) : base(_t, _sender)
        {
            selected = _selected;
            buttonID = _buttonID;
            hit = _hit;
        }
    }
    /// <summary>
    /// 脚本添加/移除事件信息类
    /// </summary>
    public class ScriptEventArgs : BaseEventArgs
    {
        /// <summary>
        /// 被处理的游戏对象
        /// </summary>
        public readonly GameObject selected;
        /// <summary>
        /// 脚本名称
        /// </summary>
        public readonly string scriptName;
        /// <summary>
        /// 脚本添加/移除事件信息类
        /// </summary>
        /// <param name="_t">事件类型</param>
        /// <param name="_sender">事件发送者</param>
        /// <param name="_selected">被处理的游戏对象</param>
        /// <param name="_scriptName">脚本名称</param>
        public ScriptEventArgs(ScriptEvent _t, GameObject _sender, GameObject _selected, string _scriptName) : base(_t, _sender)
        {
            selected = _selected;
            scriptName = _scriptName;
        }
    }

}

