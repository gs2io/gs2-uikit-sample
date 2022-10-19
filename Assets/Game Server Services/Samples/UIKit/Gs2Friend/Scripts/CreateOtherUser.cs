using System;
using System.Collections;
using Gs2.Unity.Gs2Friend.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Sample.Gs2Friend
{
    public partial class CreateOtherUser : MonoBehaviour
    {
        public Namespace Namespace;
        
        public Gs2.Unity.Gs2Account.ScriptableObject.Namespace accountNamespace;
        public Gs2.Unity.Gs2Key.ScriptableObject.Key accountKey;

        public OtherPlayer otherPlayerPrefab;
        public Transform populateNode;
        
        private Gs2ClientHolder _clientHolder;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
        }

        public void OnEnable()
        {
            IEnumerator Impl()
            {
                var future = DummyUserUtil.CreateDummyUser(accountNamespace, accountKey, _clientHolder);
                yield return future;
                if (future.Error != null)
                {
                    onError.Invoke(future.Error, null);
                    yield break;
                }
                var account = future.Result.Item1;
                var session = future.Result.Item2;

                var future2 = _clientHolder.Gs2.Friend.Namespace(
                    Namespace.namespaceName
                ).Me(
                    session
                ).Profile(
                ).UpdateProfile(
                    "public profile",
                    "follower profile",
                    "friend profile"
                );
                yield return future2;
                if (future2.Error != null)
                {
                    onError.Invoke(future2.Error, null);
                    yield break;
                }

                var otherPlayer = Instantiate(otherPlayerPrefab, populateNode);
                otherPlayer.name = account.UserId;
                otherPlayer.Set(Namespace, account.UserId);
                otherPlayer.gameObject.SetActive(true);
                
                onCreateComplete.Invoke();
            }

            StartCoroutine(Impl());
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class CreateOtherUser
    {
        [Serializable]
        private class CreateCompleteEvent : UnityEvent
        {
            
        }
        
        [SerializeField]
        private CreateCompleteEvent onCreateComplete = new CreateCompleteEvent();
        
        public event UnityAction OnCreateComplete
        {
            add => onCreateComplete.AddListener(value);
            remove => onCreateComplete.RemoveListener(value);
        }

        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();
        
        public event UnityAction<Exception, Func<IEnumerator>> OnError
        {
            add => onError.AddListener(value);
            remove => onError.RemoveListener(value);
        }
    }
}