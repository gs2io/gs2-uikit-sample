using System;
using System.Collections;
using System.Linq;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Inbox.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Sample.Gs2Inbox
{
    public partial class AllDelete : MonoBehaviour
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
            IEnumerator AllDeleteImpl()
            {
                var complete = 0;

                yield return new WaitUntil(() => _messageListFetcher.Fetched);
                
                var messages = _messageListFetcher.Messages.ToArray();
                foreach (var message in messages)
                {
                    IEnumerator DeleteImpl()
                    {
                        var future = _clientHolder.Gs2.Inbox.Namespace(
                            _messageListFetcher.Namespace.namespaceName
                        ).Me(
                            _gameSessionHolder.GameSession
                        ).Message(
                            message.Name
                        ).Delete();
                        yield return future;
                        if (future.Error != null)
                        {
                            onError.Invoke(future.Error, DeleteImpl);
                            yield break;
                        }
                        lock (this)
                        {
                            complete++;
                        }
                    }

                    StartCoroutine(DeleteImpl());
                }

                yield return new WaitUntil(() => complete == messages.Length);
                
                onComplete.Invoke();
            }

            StartCoroutine(AllDeleteImpl());
        }
    }
    
    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class AllDelete
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