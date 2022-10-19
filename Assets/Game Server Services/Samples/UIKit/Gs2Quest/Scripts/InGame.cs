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
using Gs2.Unity.Gs2Quest.Model;
using Gs2.Unity.UiKit.Gs2Quest;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Sample.Gs2Quest
{
    public class InGame : MonoBehaviour
    {
        public Gs2QuestQuestListPrefab questListPrefab;
        
        [Serializable]
        private class BackToSelectQuestGroupEvent : UnityEvent
        {
            
        }
        
        [SerializeField]
        private BackToSelectQuestGroupEvent onBackToSelectQuestGroup = new BackToSelectQuestGroupEvent();
        
        public event UnityAction OnBackToSelectQuestGroup
        {
            add => onBackToSelectQuestGroup.AddListener(value);
            remove => onBackToSelectQuestGroup.RemoveListener(value);
        }

        [Serializable]
        private class BackToSelectQuestEvent : UnityEvent
        {
            
        }
        
        [SerializeField]
        private BackToSelectQuestEvent onBackToSelectQuest = new BackToSelectQuestEvent();
        
        public event UnityAction OnBackToSelectQuest
        {
            add => onBackToSelectQuest.AddListener(value);
            remove => onBackToSelectQuest.RemoveListener(value);
        }

        public void OnBackToQuestList()
        {
            if (questListPrefab.QuestGroup == null)
            {
                onBackToSelectQuestGroup.Invoke();
            }
            else
            {
                onBackToSelectQuest.Invoke();
            }
        }
    }
}