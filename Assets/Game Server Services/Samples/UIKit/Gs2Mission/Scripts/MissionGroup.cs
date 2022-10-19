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
using Gs2.Unity.Gs2Mission.Model;
using Gs2.Unity.Gs2Mission.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gs2.Unity.UiKit.Sample.Gs2Mission
{
    [RequireComponent(typeof(Gs2MissionMissionGroupFetcher))]
    public partial class MissionGroup : MonoBehaviour
    {
        public void Update()
        {
            if (_missionGroupFetcher.Fetched)
            {
                label.text = _missionGroupFetcher.MissionGroup.Name;
            }
        }
    }
    
    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class MissionGroup
    {
        private Gs2MissionMissionGroupFetcher _missionGroupFetcher;

        public void Awake()
        {
            _missionGroupFetcher = GetComponentInParent<Gs2MissionMissionGroupFetcher>() ?? GetComponent<Gs2MissionMissionGroupFetcher>();
            Update();
        }

        public void OnClickButton()
        {
            onSelect.Invoke(_missionGroupFetcher.missionGroup.Namespace, _missionGroupFetcher.MissionGroup, onError);
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class MissionGroup
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class MissionGroup
    {
        public Text label;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class MissionGroup
    {
        [Serializable]
        private class SelectEvent : UnityEvent<Namespace, EzMissionGroupModel, ErrorEvent>
        {
            
        }
        
        [SerializeField]
        private SelectEvent onSelect = new SelectEvent();
        
        public event UnityAction<Namespace, EzMissionGroupModel, ErrorEvent> OnSelect
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