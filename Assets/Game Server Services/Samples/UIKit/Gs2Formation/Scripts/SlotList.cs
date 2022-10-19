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
using System.Collections.Generic;
using System.Linq;
using Gs2.Unity.Gs2Formation.Model;
using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.Gs2Key.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Formation.Fetcher;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gs2.Unity.UiKit.Sample.Gs2Formation
{
    [RequireComponent(typeof(Gs2FormationSlotListFetcher))]
    public partial class SlotList : MonoBehaviour
    {
        private readonly Dictionary<string, EzSlotWithSignature> _slots = new Dictionary<string, EzSlotWithSignature>();
    }
    
    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class SlotList
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2FormationSlotListFetcher _slotFetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _slotFetcher = GetComponentInParent<Gs2FormationSlotListFetcher>() ?? GetComponent<Gs2FormationSlotListFetcher>();

            IEnumerator Fetch()
            {
                yield return new WaitUntil(() => _slotFetcher.Fetched);

                foreach (var slot in _slotFetcher.Slots)
                {
                    OnSelectEquipment(
                        _slotFetcher.Models.FirstOrDefault(v => v.Name == slot.Name),
                        Gs2.Gs2Dictionary.Model.Entry.GetNamespaceNameFromGrn(slot.PropertyId),
                        Gs2.Gs2Dictionary.Model.Entry.GetEntryModelNameFromGrn(slot.PropertyId)
                    );
                }
            }

            StartCoroutine(Fetch());
        }

        public void OnSelectEquipment(
            EzSlotModel slotModel,
            Gs2.Unity.Gs2Dictionary.ScriptableObject.Entry entry
        )
        {
            var namespaceName = entry.Namespace.namespaceName;
            var entryName = entry.entryName;
            OnSelectEquipment(slotModel, namespaceName, entryName);
        }

        public void OnSelectEquipment(
            EzSlotModel slotModel, 
            string namespaceName,
            string entryName
        )
        {
            IEnumerator Fetch()
            {
                yield return new WaitUntil(() => _slotFetcher.Fetched);

                var future = _clientHolder.Gs2.Dictionary.Namespace(
                    namespaceName
                ).Me(
                    _gameSessionHolder.GameSession
                ).Entry(
                    entryName
                ).GetEntryWithSignature(
                    key.Grn
                );
                yield return future;
                if (future.Error != null)
                {
                    IEnumerator Retry()
                    {
                        yield return Fetch();
                    }
                    onError.Invoke(future.Error, Retry);
                }

                lock (_slots)
                {
                    _slots[slotModel.Name] = new EzSlotWithSignature
                    {
                        Name = slotModel.Name,
                        PropertyType = "gs2_dictionary",
                        Body = future.Result.Body,
                        Signature = future.Result.Signature,
                    };
                    onUpdateSlots.Invoke(_slots.Values.ToList());
                }
            }

            StartCoroutine(Fetch());
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class SlotList
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class SlotList
    {
        public Key key;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class SlotList
    {
        [Serializable]
        private class UpdateSlotsEvent : UnityEvent<List<EzSlotWithSignature>>
        {
            
        }
        
        [SerializeField]
        private UpdateSlotsEvent onUpdateSlots = new UpdateSlotsEvent();
        
        public event UnityAction<List<EzSlotWithSignature>> OnUpdateSlots
        {
            add => onUpdateSlots.AddListener(value);
            remove => onUpdateSlots.RemoveListener(value);
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