using System;
using System.Collections;
using System.Collections.Generic;
using Gs2.Core.Exception;
using Gs2.Unity.Gs2Account.Model;
using Gs2.Unity.Gs2Auth.Model;
using UnityEngine;

namespace Gs2.Unity.UiKit.Sample.Gs2Account
{
    public class Hint : MonoBehaviour
    {
        void Start()
        {
            Debug.Log(@"
このサンプルでは、アカウントの作成とログイン処理を実行します。 (クリックして詳細表示)
シーン内には Gs2AccountAutoLogin プレハブと Gs2ClientHolder コンポーネントを保持する Gs2 ゲームオブジェクトが配置されています。

プログラムを実行すると、Gs2ClientHolder は GS2 SDK の初期化処理を実行し Gs2ClientHolder.Instance で SDK にアクセスできる状態にします。

Gs2AccountAutoLogin プレハブ に保持される Gs2AccountAutoLogin コンポーネントはGs2ClientHolder の初期化処理が完了するのを待ち、完了したのを確認した後で以下の疑似コードに基づいてログイン処理を実行します。

---

アカウント = PlayerPrefs からアカウント情報をロード();
if (アカウント情報が見つからなかった場合) {
    アカウント = GS2-Account にアカウントを新規作成();
    PlayerPrefs にアカウント情報を保存(アカウント);
}
アクセストークン = GS2 にログイン(アカウント);

---

アクセストークン は GS2 の API を呼び出す際に必要となるゲームプレイヤーを識別するための情報です。
アクセストークン には有効期限があり、有効期限が切れる前に再取得する必要がありますが、UI Kit を利用している上では自動的に更新されますので、開発者は アクセストークン のリフレッシュについて考える必要はありません。
");
            
            Debug.Log(@"
In this sample, we will perform the account creation and login process. (Click to view details)
A Gs2 game object is placed in the scene that holds the Gs2AccountAutoLogin prefab and the Gs2ClientHolder component.

When you run the program, the Gs2ClientHolder will perform the initialization process of the GS2 SDK and make the SDK accessible in [Gs2ClientHolder.Instance].

The Gs2AccountAutoLogin component held in the Gs2AccountAutoLogin prefab waits for the initialization process of the Gs2ClientHolder to complete, and then executes the login process based on the following pseudo code after confirming that the initialization process has completed.

---

account = Load account information from PlayerPrefs();
if (account information not found) {
    account = Create a new account in GS2-Account();
    Save the account information in PlayerPrefs (account);
}
access token = Login to GS2 (Account);

---

An access token is a piece of information that identifies a game player and is required when calling the GS2 API.
The access token has an expiration date and must be reacquired before it expires, but it will be automatically updated when using the UI Kit. The developer does not need to think about refreshing the access token.
");
        }
        
        public void OnInitialize()
        {
            Debug.Log("SDK を初期化しました。");
            Debug.Log("Initialized the SDK.");
        }
        
        public void OnLoadAccount(EzAccount account)
        {
            Debug.Log("アカウント情報を読み込みました。");
            Debug.Log("Account information loaded.");
        }

        public void OnCreateAccount(EzAccount account)
        {
            Debug.Log("アカウントを新規作成しました。");
            Debug.Log("Created a new account.");
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
