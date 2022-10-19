using System;
using Gs2.Unity.Gs2Friend.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Friend.Fetcher;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gs2.Unity.UiKit.Sample.Gs2Friend
{
    [RequireComponent(typeof(Gs2FriendPublicProfileFetcher))]
    public partial class OtherPlayer : MonoBehaviour
    {
        public void Set(
            Namespace @namespace,
            string userId
        )
        {
            user = ScriptableObject.CreateInstance<User>();
            user.Namespace = @namespace;
            user.userId = userId;

            var publicProfileFetcher = GetComponent<Gs2FriendPublicProfileFetcher>();
            publicProfileFetcher.user = user;
            
            onSetUserId.Invoke(userId);
        }
        
        public void Update()
        {
            label.text = user.userId;
        }

        public void OnDestroy()
        {
            if (user != null)
            {
                Destroy(user);
                user = null;
            }
        }
    }
    
    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class OtherPlayer
    {
        
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class OtherPlayer
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class OtherPlayer
    {
        public User user;
        public Text label;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class OtherPlayer
    {
        
        [Serializable]
        private class SetUserIdEvent : UnityEvent<string>
        {
            
        }
        
        [SerializeField]
        private SetUserIdEvent onSetUserId = new SetUserIdEvent();
        
        public event UnityAction<string> OnSetUserId
        {
            add => onSetUserId.AddListener(value);
            remove => onSetUserId.RemoveListener(value);
        }

    }
}