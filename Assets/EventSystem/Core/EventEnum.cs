using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace zFrame.Event
{
    /// <summary>
    /// 笔或者鼠标事件类型
    /// </summary>
    public enum StylusEvent
    {
        Enter,
        Press,
        Release,
        Exit
    }
    /// <summary>
    /// 脚本事件类型
    /// </summary>
    public enum ScriptEvent
    {
        Amount,
        Remove
    }
    /// <summary>
    /// UI事件类型
    /// </summary>
    public enum UIEvent
    {
        PopUp
    }
}
