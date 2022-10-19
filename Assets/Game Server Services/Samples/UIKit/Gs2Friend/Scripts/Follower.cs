using Gs2.Unity.UiKit.Gs2Friend.Fetcher;
using UnityEngine;
using UnityEngine.UI;

namespace Gs2.Unity.UiKit.Sample.Gs2Friend
{
    [RequireComponent(typeof(Gs2FriendFollowFetcher))]
    public partial class Follower : MonoBehaviour
    {
        public void Update()
        {
            if (_followFetcher.Fetched)
            {
                label.text = _followFetcher.Follow.UserId;
            }
        }
    }
    
    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Follower
    {
        private Gs2FriendFollowFetcher _followFetcher;

        public void Awake()
        {
            _followFetcher = GetComponentInParent<Gs2FriendFollowFetcher>() ?? GetComponent<Gs2FriendFollowFetcher>();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Follower
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Follower
    {
        public Text label;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Follower
    {
        
    }
}