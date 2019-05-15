using UnityEngine;
namespace zFrame.Event.Example
{
    public class ColorEventDispatch : MonoBehaviour
    {

        public void ChangeColor(string HtmlColor)
        {
            Color c;
            if (ColorUtility.TryParseHtmlString(HtmlColor, out c))
            {
                EventManager.Allocate<ColorEventArgs>()
                    .Config(ColorEvent.ChangeTo, gameObject, c)
                    .Invoke();
            }
            else
            {
                Debug.Log("Html 颜色表达式格式不对！");
            }
        }
    }
}
