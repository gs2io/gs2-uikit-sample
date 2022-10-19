using UnityEditor;
using UnityEditor.Callbacks;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#if ENABLE_SIGN_IN_WITH_APPLE
using AppleAuth.Editor;
#endif
#endif

namespace Runtime.Sdk.Gs2.UiKit.Gs2Account.Editor
{
    public class AddCapability
    {
        [PostProcessBuild]
        public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
        {
            if (buildTarget == BuildTarget.iOS)
            {
#if UNITY_IOS && ENABLE_SIGN_IN_WITH_APPLE
		var projectPath = PBXProject.GetPBXProjectPath(path);
#if UNITY_2019_3_OR_NEWER
		var project = new PBXProject();
		project.ReadFromString(System.IO.File.ReadAllText(projectPath));
		var manager = new ProjectCapabilityManager(projectPath, "Entitlements.entitlements", null, project.GetUnityMainTargetGuid());
		manager.AddSignInWithAppleWithCompatibility(project.GetUnityFrameworkTargetGuid());
		manager.WriteToFile();
#else
		var manager = new ProjectCapabilityManager(projectPath, "Entitlements.entitlements", PBXProject.GetUnityTargetName());
		manager.AddSignInWithAppleWithCompatibility();
		manager.WriteToFile();
#endif
#endif
            }
        }
    }
}