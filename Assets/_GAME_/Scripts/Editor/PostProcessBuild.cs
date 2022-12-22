using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif

public static class PostProcessBuild
{
#if UNITY_IOS

        [PostProcessBuild(999)]
        public static void OnPostProcessBuild( BuildTarget buildTarget, string path)
        {
            if (buildTarget != BuildTarget.iOS) return;
            
            string plistPath = Path.Combine(path, "Info.plist");
            PlistDocument plist = new PlistDocument();

            plist.ReadFromFile(plistPath);
            plist.root.SetString("NSLocationAlwaysUsageDescription", "not used");
            plist.root.SetString("NSLocationWhenInUseUsageDescription", "not used");
            plist.root.SetString("NSCalendarsUsageDescription", "not used");
            
            var encryptKey = "ITSAppUsesNonExemptEncryption";
            plist.root.SetBoolean(encryptKey, false);
 
            // remove exit on suspend if it exists.
            var exitsOnSuspendKey = "UIApplicationExitsOnSuspend";
            if(plist.root.values.ContainsKey(exitsOnSuspendKey))
            {
                plist.root.values.Remove(exitsOnSuspendKey);
            }

            File.WriteAllText(plistPath, plist.WriteToString());
            
            var projectPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";

            var pbxProject = new PBXProject();
            pbxProject.ReadFromFile(projectPath);

            var target = pbxProject.GetUnityMainTargetGuid();          
            pbxProject.SetBuildProperty(target, "ENABLE_BITCODE", "NO");
            pbxProject.SetBuildProperty(target, "CODE_SIGN_ENTITLEMENTS", "");

            pbxProject.WriteToFile (projectPath);
        }
#endif
}