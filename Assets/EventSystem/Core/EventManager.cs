using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
namespace zFrame.Event
{
    public class EventManager
    {
        #region 单例
        /// <summary>
        /// 事件总线实例
        /// </summary>
        private static EventManager entity = null;
        public static EventManager Instance
        {
            get
            {
                if (entity == null)
                {
                    entity = new EventManager();
                }
                return entity;
            }
        }
        #endregion

        /// <summary>
        /// 事件链
        /// </summary>
        private Dictionary<Enum, List<Action<BaseEventArgs>>> eventEntitys = null;
        /// <summary>
        /// 是否中断事件分发,默认不中断
        /// </summary>
        public static bool Interrupt { get; internal set; } = false;


        private EventManager()
        {
            InitEvent();
        }

        /// <summary>
        /// 得到指定枚举项的所有事件链
        /// </summary>
        /// <param name="_type">指定枚举项</param>
        /// <returns>事件链</returns>
        private List<Action<BaseEventArgs>> GetEventList(Enum _type)
        {
            if (!eventEntitys.ContainsKey(_type))
            {
                eventEntitys.Add(_type, new List<Action<BaseEventArgs>>());
            }
            return eventEntitys[_type];
        }
        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="_type">指定类型</param>
        /// <param name="action">指定事件</param>
        private void AddEvent(Enum _type, Action<BaseEventArgs> action)
        {
            List<Action<BaseEventArgs>> actions = GetEventList(_type);
            if (!actions.Contains(action))
            {
                actions.Add(action);
            }
        }
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="_type">指定事件类型</param>
        /// <param name="args">事件参数</param>
        private void CallEvent(BaseEventArgs args)
        {
            List<Action<BaseEventArgs>> actions = GetEventList(args.m_Type);
            for (int i = actions.Count - 1; i >= 0; --i)
            {
                actions[i]?.Invoke(args);
            }
        }
        /// <summary>
        /// 删除指定的事件
        /// </summary>
        /// <param name="_type">指定类型</param>
        /// <param name="action">指定的事件</param>
        private void DelEvent(Enum _type, Action<BaseEventArgs> action)
        {
            List<Action<BaseEventArgs>> actions = GetEventList(_type);
            if (actions.Contains(action))
            {
                actions.Remove(action);
            }
        }
        /// <summary>
        /// 删除指定的事件
        /// </summary>
        /// <param name="action">指定的事件</param>
        private void DelEvent(Action<BaseEventArgs> action)
        {
            if (eventEntitys.Count > 0)
            {
                foreach (List<Action<BaseEventArgs>> actions in eventEntitys.Values)
                {
                    if (actions.Contains(action))
                    {
                        actions.Remove(action);
                    }
                }
            }
        }
        /// <summary>
        /// 初始化事件链
        /// </summary>
        private void InitEvent()
        {
            eventEntitys = new Dictionary<Enum, List<Action<BaseEventArgs>>>();
        }

        #region//--------------------------StaticFunction-------------------------------
        /// <summary>
        /// 添加事件监听
        /// </summary>
        /// <param name="_type">事件类型</param>
        /// <param name="action">事件</param>
        public static void AddListener(Enum _type, Action<BaseEventArgs> action)
        {
            if (null == entity)
            {
                entity = new EventManager();
            }
            //ValidCheck(_type); //取消事件枚举命名空间校验
            entity.AddEvent(_type, action);
        }
        /// <summary>
        /// 事件分发
        /// </summary>
        /// <param name="_type">事件类型</param>
        /// <param name="args">事件参数</param>
        public static void Invoke(BaseEventArgs args)
        {
            if (null == entity)
            {
                entity = new EventManager();
            }
            if (Interrupt)
            {
                return;
            }
            entity.CallEvent(args);
        }
        /// <summary>
        /// 移除事件监听
        /// </summary>
        /// <param name="_type">事件类型</param>
        /// <param name="action">事件</param>
        public static void DelListener(Enum _type, Action<BaseEventArgs> action)
        {
            if (null != entity)
            {
                entity.DelEvent(_type, action);
            }
        }
        /// <summary>
        /// 移除事件监听
        /// </summary>
        /// <param name="_type">事件类型</param>
        /// <param name="action">事件</param>
        public static void DelListener(Action<BaseEventArgs> action)
        {
            if (null != entity)
            {
                entity.DelEvent(action);
            }
        }
        /// <summary>
        /// 移除所有事件
        /// </summary>
        public static void RemoveAllListener()
        {
            if (null != entity)
            {
                entity.InitEvent();
            }
        }

        /// <summary>
        /// 事件枚举参数有效性检查
        /// </summary>
        [System.Obsolete("取消事件枚举校验，现在不限制枚举所在的命名空间")]
        public static void ValidCheck(Enum _type)
        {
            if (_type.GetType().Namespace != "zFrame.Event")
            {
                string msg = _type.GetType().Namespace;
                msg = "命名空间：" + (string.IsNullOrEmpty(msg) ? "无" : msg);
                throw new ArgumentException(string.Format("事件系统(纠错):事件类型必须在事件系统中有定义！Tips【{0}】", msg));
            }
        }


        #endregion//--------------------------StaticFunction-------------------------------
    }
}