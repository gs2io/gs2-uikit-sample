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
// ReSharper disable CheckNamespace

using System;
using System.Collections;
using Gs2.Core.Exception;
using Gs2.Unity.Gs2Quest.Model;
using Gs2.Unity.Gs2Quest.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Quest.Fetcher;
using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gs2.Unity.UiKit.Sample.Gs2Quest
{
    [RequireComponent(typeof(Gs2QuestQuestGroupFetcher))]
    public partial class QuestGroup : MonoBehaviour
    {
        public void Update()
        {
            if (_questGroupFetcher.Fetched)
            {
                label.text = _questGroupFetcher.QuestGroup.Name;
            }
        }
    }
    
    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class QuestGroup
    {
        private Gs2QuestQuestGroupFetcher _questGroupFetcher;

        public void Awake()
        {
            _questGroupFetcher = GetComponentInParent<Gs2QuestQuestGroupFetcher>() ?? GetComponent<Gs2QuestQuestGroupFetcher>();
            Update();
        }

        public void OnClickButton()
        {
            onSelect.Invoke(_questGroupFetcher.questGroup.Namespace, _questGroupFetcher.QuestGroup, onError);
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class QuestGroup
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class QuestGroup
    {
        public Text label;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class QuestGroup
    {
        [Serializable]
        private class SelectEvent : UnityEvent<Namespace, EzQuestGroupModel, ErrorEvent>
        {
            
        }
        
        [SerializeField]
        private SelectEvent onSelect = new SelectEvent();
        
        public event UnityAction<Namespace, EzQuestGroupModel, ErrorEvent> OnSelect
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