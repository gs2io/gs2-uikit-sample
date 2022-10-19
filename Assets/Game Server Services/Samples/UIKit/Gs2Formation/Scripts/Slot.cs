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
using Gs2.Unity.Gs2Formation.Model;
using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Formation.Fetcher;
using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gs2.Unity.UiKit.Sample.Gs2Formation
{
    [RequireComponent(typeof(Gs2FormationSlotFetcher))]
    public partial class Slot : MonoBehaviour
    {
        public void Update()
        {
            
        }
    }
    
    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Slot
    {
        private Gs2FormationSlotFetcher _slotFetcher;

        public void Awake()
        {
            _slotFetcher = GetComponentInParent<Gs2FormationSlotFetcher>() ?? GetComponent<Gs2FormationSlotFetcher>();
            Update();
        }

        public void OnClickButton()
        {
            onSelect.Invoke(_slotFetcher.slot, _slotFetcher.Model, _slotFetcher.Slot, onError);
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Slot
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Slot
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Slot
    {
        [Serializable]
        private class SelectEvent : UnityEvent<Gs2.Unity.Gs2Formation.ScriptableObject.Slot, EzSlotModel, EzSlot, ErrorEvent>
        {
            
        }
        
        [SerializeField]
        private SelectEvent onSelect = new SelectEvent();
        
        public event UnityAction<Gs2.Unity.Gs2Formation.ScriptableObject.Slot, EzSlotModel, EzSlot, ErrorEvent> OnSelect
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