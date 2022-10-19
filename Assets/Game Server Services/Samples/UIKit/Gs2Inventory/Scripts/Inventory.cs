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
using Gs2.Unity.Gs2Inventory.Model;
using Gs2.Unity.Gs2Inventory.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gs2.Unity.UiKit.Sample.Gs2Inventory
{
    [RequireComponent(typeof(Gs2InventoryInventoryFetcher))]
    public partial class Inventory : MonoBehaviour
    {
        public void Update()
        {
            if (_inventoryFetcher.Fetched)
            {
                label.text = _inventoryFetcher.Model.Name;
            }
        }
    }
    
    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Inventory
    {
        private Gs2InventoryInventoryFetcher _inventoryFetcher;

        public void Awake()
        {
            _inventoryFetcher = GetComponentInParent<Gs2InventoryInventoryFetcher>() ?? GetComponent<Gs2InventoryInventoryFetcher>();
            Update();
        }

        public void OnClickButton()
        {
            onSelect.Invoke(_inventoryFetcher.inventory.Namespace, _inventoryFetcher.Model, _inventoryFetcher.Inventory, onError);
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Inventory
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Inventory
    {
        public Text label;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Inventory
    {
        [Serializable]
        private class SelectEvent : UnityEvent<Namespace, EzInventoryModel, EzInventory, ErrorEvent>
        {
            
        }
        
        [SerializeField]
        private SelectEvent onSelect = new SelectEvent();
        
        public event UnityAction<Namespace, EzInventoryModel, EzInventory, ErrorEvent> OnSelect
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