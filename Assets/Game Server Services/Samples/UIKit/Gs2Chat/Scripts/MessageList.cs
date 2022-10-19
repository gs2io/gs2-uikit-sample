using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Gs2.Unity.UiKit.Sample.Gs2Chat
{
    public class MessageList : MonoBehaviour
    {
        public ScrollRect scrollRect;
        
        public void ScrollToLast()
        {
            IEnumerator Impl()
            {
                yield return null;
                yield return null;
                yield return null;
                yield return null;
                yield return null;
                scrollRect.verticalNormalizedPosition = 0;
            }

            StartCoroutine(Impl());
        }
    }
}