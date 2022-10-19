using System;
using System.Collections;
using Gs2.Core.Exception;
using Gs2.Unity.UiKit.Gs2Friend;
using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;
using Gs2ClientHolder = Gs2.Unity.Util.Gs2ClientHolder;

namespace Gs2.Unity.UiKit.Sample.Gs2Friend
{
    [RequireComponent(typeof(Gs2FriendSendRequest))]
    public partial class SendRequestRandomUser : MonoBehaviour
    {
        public Gs2.Unity.Gs2Account.ScriptableObject.Namespace accountNamespace;
        public Gs2.Unity.Gs2Key.ScriptableObject.Key accountKey;
        
        private Gs2ClientHolder _clientHolder;
        private Gs2FriendSendRequest _sendRequest;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _sendRequest = GetComponent<Gs2FriendSendRequest>();
            _sendRequest.enabled = false;
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
                    _sendRequest.Namespace.namespaceName
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

                _sendRequest.targetUserId = account.UserId;
                _sendRequest.enabled = true;
            }

            StartCoroutine(Impl());
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class SendRequestRandomUser
    {
        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();
        
        public event UnityAction<Gs2Exception, Func<IEnumerator>> OnError
        {
            add => onError.AddListener(value);
            remove => onError.RemoveListener(value);
        }
    }
}