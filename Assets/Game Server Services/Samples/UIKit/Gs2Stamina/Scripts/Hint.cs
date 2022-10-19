using System;
using System.Collections;
using System.Collections.Generic;
using Gs2.Core.Exception;
using Gs2.Unity.Gs2Stamina.Model;
using Gs2.Unity.Gs2Auth.Model;
using UnityEngine;

namespace Gs2.Unity.UiKit.Sample.Gs2Stamina
{
    public class Hint : MonoBehaviour
    {
        void Start()
        {
            Debug.Log(@"
このサンプルでは GS2-Stamina で管理されているスタミナの値をUIに反映します。(クリックして詳細表示)

このサンプルが依存している UI Kit は以下です。シーン内にプレハブの配置が必要です。
- Gs2AccountAutoLogin

[ Gs2StaminaLabel ]

このコンポーネントは uGUI の Text ラベルと同じ GameObject に関連づけて使用します。
すると、UI Kit は自動的に最新のスタミナの値に関する情報を Text ラベルに反映します。
Text ラベルに反映するテキストのフォーマットは以下の書式で指定が可能です。

|------------|---------------------------------------------------|
| {current}  | 現在のスタミナ値(オーバーフロー分は含まない)              |
| {total}    | 現在のスタミナ値(オーバーフロー分を含む)                 |
| {max}      | スタミナの最大値                                     |
| {overflow} | 最大値を超えて保持しているスタミナの値                   |
| {mm}       | 次回スタミナ回復までの時間(分) プレフィックスゼロパディング |
| {ss}       | 次回スタミナ回復までの時間(秒) プレフィックスゼロパディング |
| {h}        | 次回スタミナ回復までの時間(時)                         |
| {m}        | 次回スタミナ回復までの時間(分)                         |
| {s}        | 次回スタミナ回復までの時間(秒)                         |
|------------|---------------------------------------------------|

[ Gs2StaminaEnable ]

スタミナ値の状況に応じて関連づけられた GameObject を自動的に有効化・無効化するコンポーネントです。
例えば、スタミナ値が満タンの時には異なるデザインを使用したい場合や、スタミナ値が最大値を上回るオーバーフロー状態の場合には異なるデザインを使用したい場合などに
このコンポーネントを GameObject に関連づけておくと、自動的に表示する GameObject を制御してくれます。

表示・非表示の切り替え条件には以下の条件があります。

|-----------------|---------------------------------------------------------|
| Recovering      | スタミナが最大値に達していない時に有効化されます                |
| RecoveringOrMax | スタミナが最大値に達していない、もしくは最大値の時に有効化されます |
| Max             | スタミナがちょうど最大値の時に有効化されます                   |
| MaxOrOverflow   | スタミナが最大値もしくはオーバーフロー状態の時に有効化されます     |
| Overflow        | オーバーフロー状態の時に有効化されます                         |
|-----------------|---------------------------------------------------------|

[ Gs2StaminaProgress ]

スタミナの最大値に対して現在の値が何割残っているかを uGUI の Slider に反映するコンポーネントです。
Slider と同じ GameObject に関連づけてください。
");
            
            Debug.Log(@"
This sample reflects the value of stamina managed by GS2-Stamina in the UI. (Click to view details)

The UI Kit that this sample relies on is as follows A prefab needs to be placed in the scene.
- Gs2AccountAutoLogin

[ Gs2StaminaLabel ]

This component should be associated with the same GameObject as the uGUI Text label.
The UI Kit will then automatically reflect the latest stamina value information in the Text label.
The format of the text reflected in the Text label can be specified in the following format.

|------------|----------------------------------------------------------------|
| {current}  | current stamina value (not including overflow)                 |
| {total}    | current stamina value (including overflow portion)             |
| {max}      | maximum stamina value                                          |
| {overflow} | stamina value held beyond the maximum value                    |
| {mm}       | time until next stamina recovery (minutes) prefix zero padding |
| {ss}       | time until next stamina recovery (seconds) prefix zero padding |
| {h}        | time until next stamina recovery (hour)                        |
| {m}        | time until next stamina recovery (minutes)                     | 
| {s}        | time until next stamina recovery (seconds)                     | 
|------------|----------------------------------------------------------------|

[ Gs2StaminaEnable ]

A component that automatically enables or disables the associated GameObject according to the stamina value status.
For example, you may want to use a different design when the stamina value is full, or when the stamina value is in an overflow state that exceeds the maximum value.
By associating this component with a GameObject, it will automatically control which GameObject to display.

The conditions for switching between showing and hiding include the following conditions

|-----------------|-------------------------------------------------------------------------------------|
| Recovering      | Activated when stamina has not reached its maximum value                            | 
| RecoveringOrMax | Activated when stamina has not reached its maximum value or is at its maximum value |
| Max             | Activated when stamina is exactly at its maximum value                              |
| MaxOrOverflow   | Activated when stamina is at its maximum or overflow state                          |
| Overflow        | Enabled when in overflow state                                                      |
|-----------------|-------------------------------------------------------------------------------------|

[ Gs2StaminaProgress ]

A component that reflects the percentage of the current value left against the maximum value of stamina to the Slider of uGUI.
It should be associated with the same GameObject as the Slider.
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
