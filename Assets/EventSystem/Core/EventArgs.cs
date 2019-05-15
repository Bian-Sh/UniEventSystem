using System;
using UnityEngine;
namespace zFrame.Event
{
    /// <summary>
    /// 事件信息类基类
    /// </summary>
    public abstract class BaseEventArgs:IDisposable
    {
        public  Enum EventType { protected set; get; }
        public GameObject Sender { protected set; get; }
        public virtual void Config(Enum _t, GameObject _sender)
        {
            EventType = _t;
            Sender = _sender;
        }

        /// <summary>
        ///  但事件信息类被回收时调用
        /// </summary>
        public  virtual void Dispose()
        {
            this.Sender = null;
        }
    }
  
    
}

