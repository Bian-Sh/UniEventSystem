using System;
using UnityEngine;
using UnityEngine.UI;
namespace zFrame.Event.Example
{
    public class StylusEventReceive : MonoBehaviour
    {
        void Awake()
        {
            EventManager.AddListener(StylusEvent.Enter, OnPointEnter);
            EventManager.AddListener(StylusEvent.Exit, OnPointExit);
            EventManager.AddListener(StylusEvent.Press, OnPointPress);
            EventManager.AddListener(StylusEvent.Release, OnPointRelease);

            #region 以下为多种应用场景的演示
            EventManager.AddListener(StylusEvent.Enter, OnPointEnter); //演示重复添加
            EventManager.AddListener(StylusEvent.Exit, OnPointExitAddition); //演示叠加添加
            EventManager.AddListener(StylusEvent.Press, OnPointPressAddition); //叠加事件，用于演示DelLitener(Enum _type)
            EventManager.DelListener(StylusEvent.Exit, NoRegisterEvent); //演示移除未注册的事件
            EventManager.DelListener(NoRegisterEventAndNoEventTypeAssigned); //演示不指定EventType移除未注册的事件
            #endregion
        }

        private void NoRegisterEventAndNoEventTypeAssigned(BaseEventArgs obj)
        {
           
        }

        private void OnPointPressAddition(BaseEventArgs obj)
        {
            print("OnPointPressAddition ---Press [D] will remove it as all of the press event removed");
        }

        private void NoRegisterEvent(BaseEventArgs obj)
        {
        }

        private void OnPointExitAddition(BaseEventArgs obj)
        {
            print("OnPointExitAddition ~ Press 【Q】 Remove This Listener");
        }

        private void OnPointPress(BaseEventArgs obj)
        {
            StylusEventArgs args = obj as StylusEventArgs;
            SetColor(args.Selected, Color.green);
            Debug.LogFormat("鼠标按下--selected:{0},buttonID:{1}", args.Selected.name, args.ButtonID);

        }

        private void OnPointRelease(BaseEventArgs obj)
        {
            StylusEventArgs args = obj as StylusEventArgs;
            SetColor(args.Selected, Color.white);
            Debug.LogFormat("鼠标释放--selected:{0},buttonID:{1}", args.Selected.name, args.ButtonID);
        }
        private void OnPointExit(BaseEventArgs obj)
        {
            StylusEventArgs args = obj as StylusEventArgs;
            SetColor(args.Selected, Color.white);
            Debug.LogFormat("光标退出--selected:{0},buttonID:{1}", args.Selected.name, args.ButtonID);
        }

        private void OnPointEnter(BaseEventArgs obj)
        {
            StylusEventArgs args = obj as StylusEventArgs;
            SetColor(args.Selected, Color.red);
            Debug.LogFormat("光标进入--selected:{0},buttonID:{1}---Press 【R】 Remove This Listener", args.Selected.name, args.ButtonID);
        }

        public void SetColor(GameObject obj, Color color)
        {
            obj.GetComponent<MeshRenderer>().material.color = color;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("R");
                EventManager.DelListener(OnPointEnter); //不怎么建议使用，因为涉及到迭代字典并修改数据的需求，曲线救国未考虑性能问题。
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Q");
                EventManager.DelListener(StylusEvent.Exit, OnPointExitAddition);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("D");
                EventManager.DelListener(StylusEvent.Press);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("A");
                EventManager.RemoveAllListener();
            }
        }




        //我们建议在此处写上移除指定的事件，但不建议移除全部哈
        void OnDestroy()
        {
            EventManager.DelListener(StylusEvent.Enter, OnPointEnter); //移除时，可以指定事件类型
            EventManager.DelListener(OnPointExit);//移除时也可以不指定事件类型
            EventManager.RemoveAllListener(); //可以使用该方法全部移除
        }
    }
}