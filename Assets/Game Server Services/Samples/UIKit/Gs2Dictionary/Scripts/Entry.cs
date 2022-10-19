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
using Gs2.Unity.Gs2Exchange.ScriptableObject;
using Gs2.Unity.Gs2Dictionary.Model;
using Gs2.Unity.Gs2Dictionary.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Exchange.Fetcher;
using Gs2.Unity.UiKit.Gs2Dictionary.Fetcher;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Namespace = Gs2.Unity.Gs2Exchange.ScriptableObject.Namespace;

namespace Gs2.Unity.UiKit.Sample.Gs2Dictionary
{
    [RequireComponent(typeof(Gs2DictionaryEntryFetcher))]
    public partial class Entry : MonoBehaviour
    {
        
    }
    
    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Entry
    {
        private Gs2DictionaryEntryFetcher _entryFetcher;

        public void Awake()
        {
            _entryFetcher = GetComponentInParent<Gs2DictionaryEntryFetcher>() ?? GetComponent<Gs2DictionaryEntryFetcher>();
        }

        public void OnEnable()
        {
            var rate = ScriptableObject.CreateInstance<Rate>();
            rate.Namespace = Namespace;
            rate.rateName = _entryFetcher.entry.entryName;
            rateFetcher.rate = rate;
        }

        public void OnDisable()
        {
            Destroy(rateFetcher.rate);
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Entry
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Entry
    {
        public Namespace Namespace;
        public Gs2ExchangeRateFetcher rateFetcher;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Entry
    {
        
    }
}