using Gs2.Unity.UiKit.Gs2Friend.Fetcher;
using UnityEngine;
using UnityEngine.UI;

namespace Gs2.Unity.UiKit.Sample.Gs2Friend
{
    [RequireComponent(typeof(Gs2FriendSendRequestFetcher))]
    public partial class SendRequest : MonoBehaviour
    {
        public void Update()
        {
            if (_sendRequestFetcher.Fetched)
            {
                label.text = _sendRequestFetcher.SendRequest.TargetUserId;
            }
        }
    }
    
    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class SendRequest
    {
        private Gs2FriendSendRequestFetcher _sendRequestFetcher;

        public void Awake()
        {
            _sendRequestFetcher = GetComponentInParent<Gs2FriendSendRequestFetcher>() ?? GetComponent<Gs2FriendSendRequestFetcher>();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class SendRequest
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class SendRequest
    {
        public Text label;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class SendRequest
    {
        
    }
}