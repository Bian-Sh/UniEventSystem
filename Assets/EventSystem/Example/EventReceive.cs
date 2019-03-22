using UnityEngine;
using UnityEngine.UI;
namespace zFrame.Event.Example
{
    public class EventReceive : MonoBehaviour
    {
        void Awake()
        {
            EventManager.AddListener(StylusEvent.Enter, OnPointEnter);
            EventManager.AddListener(StylusEvent.Exit, OnPointExit);
            EventManager.AddListener(StylusEvent.Press, OnPointPress);
            EventManager.AddListener(StylusEvent.Release, OnPointRelease);
            EventManager.AddListener(UIEvent.PopUp, OnTipsReceive);
        }

        private void OnTipsReceive(BaseEventArgs obj)
        {
            UIEventArgs args = obj as UIEventArgs;
            Debug.Log("args.flag---" + args.flag);
            GameObject.Find("Canvas/Image/Text").GetComponent<Text>().text = (string)args.msg;
        }


        private void OnPointPress(BaseEventArgs obj)
        {
            StylusEventArgs args = obj as StylusEventArgs;
            SetColor(args.selected, Color.green);
            Debug.LogFormat("鼠标按下--selected:{0},buttonID:{1}", args.selected.name, args.buttonID);
        }

        private void OnPointRelease(BaseEventArgs obj)
        {
            StylusEventArgs args = obj as StylusEventArgs;
            SetColor(args.selected, Color.white);
            Debug.LogFormat("鼠标释放--selected:{0},buttonID:{1}", args.selected.name, args.buttonID);
        }
        private void OnPointExit(BaseEventArgs obj)
        {
            StylusEventArgs args = obj as StylusEventArgs;
            SetColor(args.selected, Color.white);
            Debug.LogFormat("光标退出--selected:{0},buttonID:{1}", args.selected.name, args.buttonID);
        }

        private void OnPointEnter(BaseEventArgs obj)
        {
            StylusEventArgs args = obj as StylusEventArgs;
            SetColor(args.selected, Color.red);
            Debug.LogFormat("光标进入--selected:{0},buttonID:{1}", args.selected.name, args.buttonID);
        }

        public void SetColor(GameObject obj, Color color)
        {
            obj.GetComponent<MeshRenderer>().material.color = color;
        }
        //----------------下面用来演示移除事件监听
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("q");
                EventManager.DelListener(StylusEvent.Enter, OnPointEnter); //移除时，可以指定事件类型
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("w");
                EventManager.DelListener(OnPointExit);//移除时也可以不指定事件类型
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E");
                EventManager.RemoveAllListener(); //可以使用该方法全部移除
            }
        }
    }
    namespace MyNamespace
    {
        public enum TestNum { test }
    }
}