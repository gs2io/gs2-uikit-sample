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
    [RequireComponent(typeof(Gs2FormationFormFetcher))]
    public partial class Form : MonoBehaviour
    {
        public void Update()
        {
            
        }
    }
    
    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Form
    {
        private Gs2FormationFormFetcher _formFetcher;

        public void Awake()
        {
            _formFetcher = GetComponentInParent<Gs2FormationFormFetcher>() ?? GetComponent<Gs2FormationFormFetcher>();
            Update();
        }

        public void OnClickButton()
        {
            onSelect.Invoke(_formFetcher.form, _formFetcher.Model, _formFetcher.Form, onError);
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Form
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Form
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Form
    {
        [Serializable]
        private class SelectEvent : UnityEvent<Gs2.Unity.Gs2Formation.ScriptableObject.Form, EzFormModel, EzForm, ErrorEvent>
        {
            
        }
        
        [SerializeField]
        private SelectEvent onSelect = new SelectEvent();
        
        public event UnityAction<Gs2.Unity.Gs2Formation.ScriptableObject.Form, EzFormModel, EzForm, ErrorEvent> OnSelect
        {
            add => onSelect.AddListener(value);
            remove => onSelect.RemoveListener(value);
        }
        
        [SerializeField]
        private ErrorEvent onError = new ErrorEvent();
        
        public event UnityAction<Exception, Func<IEnumerator>> OnError
        {
            add => onError.AddListener(value);
            remove => onError.RemoveListener(value);
        }
    }
}