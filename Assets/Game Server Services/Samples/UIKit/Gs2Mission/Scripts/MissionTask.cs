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

using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEngine;
using UnityEngine.UI;

namespace Gs2.Unity.UiKit.Sample.Gs2Mission
{
    [RequireComponent(typeof(Gs2MissionMissionTaskFetcher))]
    public class MissionTask : MonoBehaviour
    {
        private Gs2MissionMissionTaskFetcher _missionTaskFetcher;

        public Text label;

        public void Awake()
        {
            _missionTaskFetcher = transform.GetComponentInParent<Gs2MissionMissionTaskFetcher>();
            Update();
        }

        public void Update()
        {
            if (_missionTaskFetcher.Fetched)
            {
                label.text = _missionTaskFetcher.Task.Name;
            }
        }
    }
}