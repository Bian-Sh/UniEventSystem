using UnityEngine;
namespace zFrame.Event.Example
{
    public class EventDispatch : MonoBehaviour
    {
        private GameObject selected;
        void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                GameObject cached = hit.collider.gameObject;
                if (null == selected)//从无到有
                {
                    selected = cached;
                    EventManager.Invoke(new StylusEventArgs(StylusEvent.Enter, gameObject, selected));
                }
                else  //从有到有
                {
                    if (selected != cached)
                    {
                        EventManager.Invoke(new StylusEventArgs(StylusEvent.Exit, gameObject, selected));
                        selected = cached;
                        EventManager.Invoke(new StylusEventArgs(StylusEvent.Enter, gameObject, selected));
                    }
                }

                if (Input.GetMouseButtonDown(0))
                {
                    EventManager.Invoke(new StylusEventArgs(StylusEvent.Press, gameObject, selected, 0));
                }
                if (Input.GetMouseButtonUp(0))
                {
                    EventManager.Invoke(new StylusEventArgs(StylusEvent.Release, gameObject, selected, 0));
                }

            }
            else
            {
                if (null != selected)
                {
                    EventManager.Invoke(new StylusEventArgs(StylusEvent.Exit, gameObject, selected));
                    selected = null;
                }
            }
        }

        public void OnButtonClick()
        {
            EventManager.Invoke(new UIEventArgs(UIEvent.PopUp, gameObject, "你点击了一个按键，需要搞事情了！"));
        }
    }
}
