using UnityEngine;
namespace zFrame.Event.Example
{
    public class EventDispatch : MonoBehaviour
    {
        private GameObject selected;
        public LayerMask layerMask = 1;
        public float maxDistance = 100;
        void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit,maxDistance,layerMask))
            {
                GameObject cached = hit.collider.gameObject;
                if (null == selected)//从无到有
                {
                    selected = cached;
                    EventManager.Allocate<StylusEventArgs>()
                        .Config(StylusEvent.Enter, gameObject, selected)
                        .Invoke();
                }
                else  //从有到有
                {
                    if (selected != cached)
                    {
                        EventManager.Allocate<StylusEventArgs>()
                             .Config(StylusEvent.Exit, gameObject, selected)
                             .Invoke();
                        selected = cached;
                        EventManager.Allocate<StylusEventArgs>()
                            .Config(StylusEvent.Enter, gameObject, selected)
                            .Invoke();
                    }
                }

                if (Input.GetMouseButtonDown(0))
                {
                    EventManager.Allocate<StylusEventArgs>()
                        .Config(StylusEvent.Press, gameObject, selected, 0)
                        .Invoke();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    EventManager.Allocate<StylusEventArgs>()
                        .Config(StylusEvent.Release, gameObject, selected, 0)
                        .Invoke();
                }

            }
            else
            {
                if (null != selected)
                {
                    EventManager.Allocate<StylusEventArgs>()
                        .Config(StylusEvent.Exit, gameObject, selected)
                        .Invoke();
                    selected = null;
                }
            }
        }
    }
}
