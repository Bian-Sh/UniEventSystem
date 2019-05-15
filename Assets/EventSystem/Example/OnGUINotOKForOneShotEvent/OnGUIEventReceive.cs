using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using zFrame.Event;

public class OnGUIEventReceive : MonoBehaviour {

	void Start () {
        EventManager.AddListener(StylusEvent.Press,OnStylusPressed);
	}

    private void OnStylusPressed(BaseEventArgs obj)
    {
        StylusEventArgs args = obj as StylusEventArgs;
        GameObject go = args.Selected;
        if (null!=go)
        {
            Debug.Log(go.name);
        }
        else
        {
            Debug.Log("you receive a null reference");
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
