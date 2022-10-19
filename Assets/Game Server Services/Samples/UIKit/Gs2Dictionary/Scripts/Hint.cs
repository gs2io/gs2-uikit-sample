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
using Gs2.Unity.Gs2Auth.Model;
using UnityEngine;

namespace Gs2.Unity.UiKit.Sample.Gs2Dictionary
{
    public class Hint : MonoBehaviour
    {
        void Start()
        {
            Debug.Log(@"
");
            
            Debug.Log(@"
");
        }

        public void OnInitialize()
        {
            Debug.Log("SDK を初期化しました。");
            Debug.Log("Initialized the SDK.");
        }
        
        public void OnLoggedIn(EzAccessToken accessToken)
        {
            Debug.Log("アクセストークンを取得しました。");
            Debug.Log("Access token has been obtained.");
        }

        public void OnError(Exception e, Func<IEnumerator> retry)
        {
            Debug.Log("エラーが発生しました。");
            Debug.Log("An error has occurred.");
            Debug.LogError(e);
        }
    }
}
