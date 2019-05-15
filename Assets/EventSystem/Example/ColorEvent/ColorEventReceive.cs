using System;
using UnityEngine;
using UnityEngine.UI;
namespace zFrame.Event.Example
{
    public class ColorEventReceive : MonoBehaviour
    {
        void Awake()
        {
            EventManager.AddListener(ColorEvent.ChangeTo, OnColorChangeRequired);
        }
        private void OnColorChangeRequired(BaseEventArgs obj)
        {
            ColorEventArgs args = obj as ColorEventArgs;
            GetComponent<MeshRenderer>().material.color = args.Color;
        }
    }
}