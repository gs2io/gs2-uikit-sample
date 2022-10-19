using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
#if UNITY_IOS 
using UnityEditor.iOS.Xcode;
#endif
using UnityEngine;

namespace Runtime.Sdk.Gs2.UiKit.Gs2Account.Editor
{
    public class AddSignInWithGoogle
    {
#if UNITY_IOS
        // GoogleSignInのクライアントIDを設定してください
        private const string clientId = "com.googleusercontent.apps.xxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
#endif
        
        [PostProcessBuild]
        public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
        {
            if (buildTarget == BuildTarget.iOS)
            {
#if UNITY_IOS
                var projPath = PBXProject.GetPBXProjectPath(path);
                {
                    var document = new PlistDocument();
                    document.ReadFromFile(Path.Combine(path, "Info.plist"));

                    document.root
                        .CreateArray("CFBundleURLTypes").AddDict()
                        .CreateArray("CFBundleURLSchemes")
                        .AddString(clientId);

                    document.WriteToFile(Path.Combine(path, "Info.plist"));
                }
                {
                    string googleServiceInfoPath = "GoogleService-Info.plist";
                    string copyFrom = Path.Combine(Application.dataPath, "Game Server Services/Samples/UIKit/Gs2Account/Editor/" + googleServiceInfoPath);
                    string copyTo = Path.Combine(path, googleServiceInfoPath);

                    FileUtil.DeleteFileOrDirectory(copyTo);
                    FileUtil.CopyFileOrDirectory(copyFrom, copyTo);

                    var proj = new PBXProject();
                    proj.ReadFromFile(projPath);
                    {
                        var target = proj.GetUnityMainTargetGuid();
                        proj.AddFileToBuild(target, proj.AddFile(googleServiceInfoPath, googleServiceInfoPath));
                    }
                    {
                        var target = proj.GetUnityFrameworkTargetGuid();
                        proj.AddFileToBuild(target, proj.AddFile(googleServiceInfoPath, googleServiceInfoPath));
                    }
                    proj.WriteToFile(projPath);
                }
#endif
            }
        }
    }
}