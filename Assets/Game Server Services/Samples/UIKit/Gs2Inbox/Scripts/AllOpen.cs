using System;
using System.Collections;
using System.Linq;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Inbox.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Sample.Gs2Inbox
{
    public partial class AllOpen : MonoBehaviour
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2InboxMessageListFetcher _messageListFetcher;
        
        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _messageListFetcher = GetComponentInParent<Gs2InboxMessageListFetcher>() ?? GetComponent<Gs2InboxMessageListFetcher>();
        }

        public void OnEnable()
        {
            IEnumerator AllReadImpl()
            {
                var complete = 0;

                yield return new WaitUntil(() => _messageListFetcher.Fetched);
                
                var unreadMessages = _messageListFetcher.Messages.Where(v => !v.IsRead).ToArray();
                foreach (var unreadMessage in unreadMessages)
                {
                    IEnumerator ReadImpl()
                    {
                        var future = _clientHolder.Gs2.Inbox.Namespace(
                            _messageListFetcher.Namespace.namespaceName
                        ).Me(
                            _gameSessionHolder.GameSession
                        ).Message(
                            unreadMessage.Name
                        ).Read();
                        yield return future;
                        if (future.Error != null)
                        {
                            onError.Invoke(future.Error, ReadImpl);
                            yield break;
                        }
                        lock (this)
                        {
                            complete++;
                        }
                    }

                    StartCoroutine(ReadImpl());
                }

                yield return new WaitUntil(() => complete == unreadMessages.Length);
                
                onComplete.Invoke();
            }

            StartCoroutine(AllReadImpl());
        }
    }
    
    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class AllOpen
    {
        [Serializable]
        private class CompleteEvent : UnityEvent
        {
            
        }
        
        [SerializeField]
        private CompleteEvent onComplete = new CompleteEvent();
        
        public event UnityAction OnComplete
        {
            add => onComplete.AddListener(value);
            remove => onComplete.RemoveListener(value);
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