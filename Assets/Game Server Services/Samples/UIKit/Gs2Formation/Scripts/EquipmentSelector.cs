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
using Gs2.Unity.Gs2Formation.Model;
using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Formation.Fetcher;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gs2.Unity.UiKit.Sample.Gs2Formation
{
    [RequireComponent(typeof(Gs2FormationSlotFetcher))]
    public partial class EquipmentSelector : MonoBehaviour
    {
        public void Update()
        {
            
        }
    }
    
    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class EquipmentSelector
    {
        private Gs2FormationSlotFetcher _slotFetcher;

        public void Set(
            Gs2.Unity.Gs2Formation.ScriptableObject.Slot slotKey, 
            EzSlotModel slotModel, 
            EzSlot slot, 
            ErrorEvent onError
        )
        {
            var slotFetcher = GetComponentInParent<Gs2FormationSlotFetcher>() ?? GetComponent<Gs2FormationSlotFetcher>();
            slotFetcher.slot = slotKey;
        }
        
        public void Awake()
        {
            _slotFetcher = GetComponentInParent<Gs2FormationSlotFetcher>() ?? GetComponent<Gs2FormationSlotFetcher>();
            Update();
        }

        public void OnSelect(Gs2.Unity.Gs2Dictionary.ScriptableObject.Entry entry)
        {
            onSelectEquipment.Invoke(_slotFetcher.Model, entry);
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class EquipmentSelector
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class EquipmentSelector
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class EquipmentSelector
    {
        [Serializable]
        private class SelectEquipmentEvent : UnityEvent<EzSlotModel, Gs2.Unity.Gs2Dictionary.ScriptableObject.Entry>
        {
            
        }
        
        [SerializeField]
        private SelectEquipmentEvent onSelectEquipment = new SelectEquipmentEvent();
        
        public event UnityAction<EzSlotModel, Gs2.Unity.Gs2Dictionary.ScriptableObject.Entry> OnSelectEquipment
        {
            add => onSelectEquipment.AddListener(value);
            remove => onSelectEquipment.RemoveListener(value);
        }
    }
}