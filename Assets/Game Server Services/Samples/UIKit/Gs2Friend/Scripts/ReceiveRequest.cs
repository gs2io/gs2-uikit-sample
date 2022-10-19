using Gs2.Unity.UiKit.Gs2Friend.Fetcher;
using UnityEngine;
using UnityEngine.UI;

namespace Gs2.Unity.UiKit.Sample.Gs2Friend
{
    [RequireComponent(typeof(Gs2FriendReceiveRequestFetcher))]
    public partial class ReceiveRequest : MonoBehaviour
    {
        public void Update()
        {
            if (_sendRequestFetcher.Fetched)
            {
                label.text = _sendRequestFetcher.ReceiveRequest.UserId;
            }
        }
    }
    
    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class ReceiveRequest
    {
        private Gs2FriendReceiveRequestFetcher _sendRequestFetcher;

        public void Awake()
        {
            _sendRequestFetcher = GetComponentInParent<Gs2FriendReceiveRequestFetcher>() ?? GetComponent<Gs2FriendReceiveRequestFetcher>();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class ReceiveRequest
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class ReceiveRequest
    {
        public Text label;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class ReceiveRequest
    {
        
    }
}