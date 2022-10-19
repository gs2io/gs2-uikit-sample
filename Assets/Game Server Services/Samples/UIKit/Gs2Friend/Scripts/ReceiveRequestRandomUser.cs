/*
 * Copyright 2016 Game Server Services, Inc. or its affiliates. All Rights
 * Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable CheckNamespace

using System;
using System.Collections;
using Gs2.Core.Exception;
using Gs2.Unity.Gs2Friend.ScriptableObject;
using UnityEngine;
using UnityEngine.Events;
using ErrorEvent = Gs2.Unity.Util.ErrorEvent;

namespace Gs2.Unity.UiKit.Sample.Gs2Friend
{
    public partial class ReceiveRequestRandomUser : MonoBehaviour
    {
        public Gs2.Unity.Gs2Account.ScriptableObject.Namespace accountNamespace;
        public Gs2.Unity.Gs2Key.ScriptableObject.Key accountKey;
        
        private IEnumerator Process()
        {
            var future2 = DummyUserUtil.CreateDummyUser(accountNamespace, accountKey, _clientHolder);
            yield return future2;
            if (future2.Error != null)
            {
                onError.Invoke(future2.Error, null);
                yield break;
            }
            var account = future2.Result.Item1;
            var session = future2.Result.Item2;

            var future3 = _clientHolder.Gs2.Friend.Namespace(
                Namespace.namespaceName
            ).Me(
                session
            ).Profile(
            ).UpdateProfile(
                "public profile",
                "follower profile",
                "friend profile"
            );
            yield return future3;
            if (future3.Error != null)
            {
                onError.Invoke(future3.Error, null);
                yield break;
            }
            
            var future = _clientHolder.Gs2.Friend.Namespace(
                Namespace.namespaceName
            ).Me(
                session
            ).SendRequest(
                _gameSessionHolder.GameSession.AccessToken.UserId
            );
            yield return future;
            if (future.Error != null)
            {
                if (future.Error is TransactionException e)
                {
                    IEnumerator Retry()
                    {
                        var retryFuture = e.Retry();
                        yield return retryFuture;
                        if (retryFuture.Error != null)
                        {
                            onError.Invoke(future.Error, Retry);
                            yield break;
                        }
                        onSendRequestComplete.Invoke(this);
                    }

                    onError.Invoke(future.Error, Retry);
                    yield break;
                }

                onError.Invoke(future.Error, null);
                yield break;
            }
            
            _clientHolder.Gs2.ClearCache();
            
            onSendRequestComplete.Invoke(this);
        }
        
        public void OnEnable()
        {
            StartCoroutine(nameof(Process));
        }
        
        public void OnDisable()
        {
            StopCoroutine(nameof(Process));
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class ReceiveRequestRandomUser
    {
        private Gs2.Unity.Util.Gs2ClientHolder _clientHolder;
        private Gs2.Unity.Util.Gs2GameSessionHolder _gameSessionHolder;

        public void Awake()
        {
            _clientHolder = Gs2.Unity.Util.Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2.Unity.Util.Gs2GameSessionHolder.Instance;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class ReceiveRequestRandomUser
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class ReceiveRequestRandomUser
    {
        public Namespace Namespace;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class ReceiveRequestRandomUser
    {
        [Serializable]
        private class SendRequestCompleteEvent : UnityEvent<ReceiveRequestRandomUser>
        {
            
        }
        
        [SerializeField]
        private SendRequestCompleteEvent onSendRequestComplete = new SendRequestCompleteEvent();
        
        public event UnityAction<ReceiveRequestRandomUser> OnSendRequestComplete
        {
            add => onSendRequestComplete.AddListener(value);
            remove => onSendRequestComplete.RemoveListener(value);
        }

        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();
        
        public event UnityAction<Gs2Exception, Func<IEnumerator>> OnError
        {
            add => onError.AddListener(value);
            remove => onError.RemoveListener(value);
        }
    }
}