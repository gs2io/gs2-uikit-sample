using System;
using System.Collections;
using System.Collections.Generic;
using Gs2.Core.Exception;
using Gs2.Unity.Gs2Account.Model;
using Gs2.Unity.Gs2Auth.Model;
using UnityEngine;

namespace Gs2.Unity.UiKit.Sample.Gs2Money
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
