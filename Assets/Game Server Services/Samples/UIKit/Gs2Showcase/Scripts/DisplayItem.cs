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

using System.Collections;
using Gs2.Unity.UiKit.Gs2Showcase.Fetcher;
using UnityEngine;
using UnityEngine.UI;

namespace Gs2.Unity.UiKit.Sample.Gs2Showcase
{
    [RequireComponent(typeof(Gs2ShowcaseDisplayItemFetcher))]
    public class DisplayItem : MonoBehaviour
    {
        private Gs2ShowcaseDisplayItemFetcher _displayItemFetcher;

        public Text label;

        public void Awake()
        {
            _displayItemFetcher = transform.GetComponentInParent<Gs2ShowcaseDisplayItemFetcher>();
        }

        public IEnumerator Start()
        {
            yield return new WaitUntil(() => _displayItemFetcher.Fetched);
            label.text = _displayItemFetcher.DisplayItem.SalesItem.Name;
        }
    }
}