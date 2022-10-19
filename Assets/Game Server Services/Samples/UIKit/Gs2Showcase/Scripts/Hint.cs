using System;
using System.Collections;
using System.Collections.Generic;
using Gs2.Core.Exception;
using Gs2.Unity.Gs2Showcase.Model;
using Gs2.Unity.Gs2Auth.Model;
using UnityEngine;

namespace Gs2.Unity.UiKit.Sample.Gs2Showcase
{
    public class Hint : MonoBehaviour
    {
        void Start()
        {
            Debug.Log(@"
このサンプルでは GS2-Showcase で管理されているゲーム内ストアをUIに反映します。（クリックして詳細を表示）

このサンプルが依存している UI Kit は以下です。シーン内にプレハブの配置が必要です。
- Gs2AccountAutoLogin

[ Gs2ShowcaseShowcase ]

陳列棚に陳列された商品を UI に反映するコンポーネントです。
指定した陳列棚に陳列されている商品の一覧を取得し、商品ごとに DisplayItemPrefab で指定した Prefab を実体化し、
PopulateNode で指定した Transform の子供として登録します。

[ Gs2ShowcaseDisplayItem ]

陳列棚に登録された商品ごとに作成される Prefab のルート階層に指定するコンポーネントです。
このコンポーネントには void Buy() メソッドが実装されており、このメソッドを呼び出すことで商品を購入できます。
本サンプルでは Button の OnClick イベントでこのメソッドを呼び出しています。

Buy メソッドはコルーチンを起動して商品の購入処理を実行するため、コルーチンが多重起動されることを防ぐためにボタンを押した後にボタンを無効化する必要があります。
このコンポーネントは購入処理が完了した時にイベントを発行する OnBuyComplete イベントを登録することができます。
このイベントをハンドリングして、商品購入後のシーケンスに遷移したり、ボタンを再度有効化したりすることができます。
");
            
            Debug.Log(@"
In this sample, the UI reflects the in-game store managed by GS2-Showcase. (Click to view details)

The UI Kit that this sample relies on is as follows You need to place a prefab in your scene.
- Gs2AccountAutoLogin

[ Gs2ShowcaseShowcase ]

This component reflects the products displayed on the display shelf in the UI.
It acquires the list of items displayed on the specified display shelf, materializes the Prefab specified in DisplayItemPrefab for each item, and displays the list in the UI.
For each item, it materializes the Prefab specified in DisplayItemPrefab and registers it as a child of the Transform specified in PopulateNode.

[ Gs2ShowcaseDisplayItem ]

This component is specified in the root hierarchy of the Prefab that is created for each item registered in the display shelf.
This component implements the void Buy() method, and by calling this method, you can purchase the item.
In this sample, this method is called in the Button's OnClick event.

Since the Buy method invokes a coroutine to execute the product purchase process, it is necessary to disable the button after pressing it to prevent multiple invocations of the coroutine.
This component can register an OnBuyComplete event that will fire an event when the purchase process is complete.
This event can be handled to transition to the post-purchase sequence of the product or to re-enable the button.
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
