using System;
using System.Collections;
using Gs2.Core.Domain;
using Gs2.Core.Net;
using Gs2.Unity.Gs2Account.Model;
using Gs2.Unity.Gs2Key.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.Util;
using Gs2ClientHolder = Gs2.Unity.Util.Gs2ClientHolder;
using Namespace = Gs2.Unity.Gs2Account.ScriptableObject.Namespace;

namespace Gs2.Unity.UiKit.Sample.Gs2Friend
{
    public class DummyUserUtil
    {
        public static IFuture<Tuple<EzAccount, GameSession>> CreateDummyUser(Namespace @namespace, Key key, Gs2ClientHolder clientHolder)
        {
            IEnumerator Impl(Gs2Future<Tuple<EzAccount, GameSession>> self)
            {
                var future2 = clientHolder.Gs2.Account.Namespace(
                    @namespace.namespaceName
                ).Create();
                yield return future2;
                if (future2.Error != null)
                {
                    self.OnError(future2.Error);
                    yield break;
                }
                var future3 = future2.Result.Model();
                yield return future3;
                if (future3.Error != null)
                {
                    self.OnError(future3.Error);
                    yield break;
                }

                var fromUser = future3.Result;
                var future4 = future2.Result.Authentication(
                    key.Grn,
                    fromUser.Password
                );
                yield return future4;
                if (future4.Error != null)
                {
                    self.OnError(future4.Error);
                    yield break;
                }

                var future5 = clientHolder.Gs2.Auth.AccessToken().Login(
                    key.Grn,
                    future4.Result.Body,
                    future4.Result.Signature
                );
                yield return future5;
                if (future5.Error != null)
                {
                    self.OnError(future5.Error);
                    yield break;
                }

                var future6 = future5.Result.Model();
                yield return future6;
                if (future6.Error != null)
                {
                    self.OnError(future6.Error);
                    yield break;
                }
                
                self.OnComplete(new Tuple<EzAccount, GameSession>(future3.Result, future6.Result));
            }
            return new Gs2InlineFuture<Tuple<EzAccount, GameSession>>(Impl);
        }
    }
}