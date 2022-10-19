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
 * perinventorys and limitations under the License.
 */
// ReSharper disable CheckNamespace

using System;
using System.Collections;
using Gs2.Core.Exception;
using Gs2.Unity.Gs2Chat.Model;
using Gs2.Unity.Gs2Chat.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Chat.Fetcher;
using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gs2.Unity.UiKit.Sample.Gs2Chat
{
    [RequireComponent(typeof(Gs2ChatRoomFetcher))]
    public partial class Room : MonoBehaviour
    {
        public void Update()
        {
            if (_roomFetcher.Fetched)
            {
                if (_roomFetcher.Room != null)
                    label.text = _roomFetcher.Room.Name;
            }
        }
    }
    
    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Room
    {
        private Gs2ChatRoomFetcher _roomFetcher;

        public void Awake()
        {
            _roomFetcher = GetComponentInParent<Gs2ChatRoomFetcher>() ?? GetComponent<Gs2ChatRoomFetcher>();
            Update();
        }

        public void OnClickButton()
        {
            onSelect.Invoke(_roomFetcher.room.Namespace, _roomFetcher.Room);
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Room
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Room
    {
        public Text label;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Room
    {
        [Serializable]
        private class SelectEvent : UnityEvent<Namespace, EzRoom>
        {
            
        }
        
        [SerializeField]
        private SelectEvent onSelect = new SelectEvent();
        
        public event UnityAction<Namespace, EzRoom> OnSelect
        {
            add => onSelect.AddListener(value);
            remove => onSelect.RemoveListener(value);
        }
        
        [SerializeField]
        private ErrorEvent onError = new ErrorEvent();
        
        public event UnityAction<Gs2Exception, Func<IEnumerator>> OnError
        {
            add => onError.AddListener(value);
            remove => onError.RemoveListener(value);
        }
    }
}