using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using zFrame.Event;

public class EventDispatchInOnGUI : MonoBehaviour {

    public Transform target;
	void Start () {
		
	}
    private void OnGUI()
    {
        if (Event.current.isMouse&&Event.current.button==0&&Event.current.clickCount==2) //测试左键双击发事件
        {
            EventManager.Allocate<StylusEventArgs>()
                .Config( StylusEvent.Press,gameObject,target.gameObject)
                .Invoke();
        }
    }

}
