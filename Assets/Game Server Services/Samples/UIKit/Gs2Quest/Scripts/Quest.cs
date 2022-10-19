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
using Gs2.Unity.UiKit.Gs2Quest.Fetcher;
using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gs2.Unity.UiKit.Sample.Gs2Quest
{
    [RequireComponent(typeof(Gs2QuestQuestFetcher))]
    public partial class Quest : MonoBehaviour
    {
        public void Update()
        {
            if (_questFetcher.Fetched)
            {
                label.text = _questFetcher.Quest.Name;
            }
        }
    }
    
    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Quest
    {
        private Gs2QuestQuestFetcher _questFetcher;

        public void Awake()
        {
            _questFetcher = GetComponentInParent<Gs2QuestQuestFetcher>() ?? GetComponent<Gs2QuestQuestFetcher>();
            Update();
        }

        public void OnClickButton()
        {
            onSelect.Invoke(_questFetcher.quest.questGroup, _questFetcher.Quest, onError);
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Quest
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Quest
    {
        public Text label;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Quest
    {
        [Serializable]
        private class SelectEvent : UnityEvent<Gs2.Unity.Gs2Quest.ScriptableObject.QuestGroup, EzQuestModel, ErrorEvent>
        {
            
        }
        
        [SerializeField]
        private SelectEvent onSelect = new SelectEvent();
        
        public event UnityAction<Gs2.Unity.Gs2Quest.ScriptableObject.QuestGroup, EzQuestModel, ErrorEvent> OnSelect
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