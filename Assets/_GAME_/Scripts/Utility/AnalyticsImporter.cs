using System;
using GameAnalyticsSDK;
using GameAnalyticsSDK.Events;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class AnalyticsImporter : MonoBehaviour
{
    public GameAnalytics gameAnalyticsPrefab;
    // [InfoBox("Adjust will not be initialized since the token is null.", VisibleIf = "CanShowInfoBox")]
    // [ShowIf("IsAndroid")]
    // [SerializeField] private string _androidToken;
    // [ShowIf("IsIOS")]
    // [SerializeField] private string _iosToken;
    
#if UNITY_EDITOR
    // public bool IsAndroid => UnityEditor.EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android;
    // public bool IsIOS => UnityEditor.EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS;
    // public bool IsAndroidTokenEmpty => string.IsNullOrEmpty(_androidToken);
    // public bool IsIOSTokenEmpty => string.IsNullOrEmpty(_iosToken);
    // public bool CanShowInfoBox => (IsAndroid && IsAndroidTokenEmpty) || (IsIOS && IsIOSTokenEmpty);
    // public string AndroidToken => _androidToken;
    // public string IOSToken => _iosToken;
    #endif
    private void Start()
    {
// #if UNITY_IOS
//         InitAdjust(_iosToken);
// #elif UNITY_ANDROID
//         InitAdjust(_androidToken);
// #endif

        InitFacebook();
        InitGameAnalytics();
    }

    private void InitGameAnalytics()
    {
        var gameAnalyticsInstance = FindObjectOfType<GameAnalytics>();
        if (gameAnalyticsInstance == null)
        {
            gameAnalyticsInstance = Instantiate(gameAnalyticsPrefab);
            gameAnalyticsInstance.gameObject.SetActive(true);
        }
        GameAnalytics.Initialize();
    }

    private void InitFacebook()
    {
        var go  = new GameObject("FacebookInit").AddComponent<FacebookInit>();
        DontDestroyOnLoad(go);
    }

    // private void InitAdjust(string adjustAppToken)
    // {
    //     //Don't initialize if the string is empty
    //     if (string.IsNullOrEmpty(adjustAppToken))
    //     {
    //         return;
    //     }
    //     
    //     var adjustConfig = new AdjustConfig(
    //         adjustAppToken,
    //         AdjustEnvironment.Production,
    //         true
    //     );
    //     
    //     adjustConfig.setLogLevel(AdjustLogLevel.Info);
    //     adjustConfig.setSendInBackground(true);
    //     var go = new GameObject("Adjust").AddComponent<Adjust>();
    //     DontDestroyOnLoad(go);
    //     Adjust.start(adjustConfig);
    // }
}