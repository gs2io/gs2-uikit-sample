using Gs2.Unity.UiKit.Gs2Friend.Fetcher;
using UnityEngine;
using UnityEngine.UI;

namespace Gs2.Unity.UiKit.Sample.Gs2Friend
{
    [RequireComponent(typeof(Gs2FriendFriendFetcher))]
    public partial class Friend : MonoBehaviour
    {
        public void Update()
        {
            if (_friendFetcher.Fetched)
            {
                label.text = _friendFetcher.Friend.UserId;
            }
        }
    }
    
    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Friend
    {
        private Gs2FriendFriendFetcher _friendFetcher;

        public void Awake()
        {
            _friendFetcher = GetComponentInParent<Gs2FriendFriendFetcher>() ?? GetComponent<Gs2FriendFriendFetcher>();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Friend
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Friend
    {
        public Text label;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Friend
    {
        
    }
}