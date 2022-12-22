using System;
using System.Collections.Generic;
using Facebook.Unity.Settings;
using GameAnalyticsSDK.Setup;
using Ludiq.PeekCore;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class PrereleaseCheckList : OdinEditorWindow
{
	
	[MenuItem("OmnioCore/Prerelease Checklist")]
	private static void ShowWindow()
	{
		var window = GetWindow<PrereleaseCheckList>();
		window.titleContent = new GUIContent("Prerelease Checklist");
		window.Show();
	}

	private void OnFocus()
	{
		CheckAllSettings();
	}

	private void CheckAllSettings()
	{
		CheckFacebookSettings();
		CheckElephantSettings();
		CheckGameAnalyticsSettings();
		CheckTargetAPILevel();
		CheckPackageName();
		CheckUnitySplash();
		CheckBuildAppBundle();
		CheckFrameRate();
		CheckDebugMode();
	}

	#region Facebook Check
	[InfoBox(FACEBOOK_ID_ERROR_MESSAGE, InfoMessageType.Error, VisibleIf = nameof(IsFacebookIdNullOrDefault))]
	[SerializeField, BoxGroup(FACEBOOK_SETTINGS), LabelText(""), GUIColor(nameof(FacebookColor))]
	private string FacebookId;

	private const string FACEBOOK_SETTINGS = "Facebook Settings";
	private const string FACEBOOK_ID_ERROR_MESSAGE = "Facebook Id is either null or set to default template value, please check Facebook Settings";
	private bool IsFacebookIdNullOrDefault => string.IsNullOrEmpty(FacebookId) || FacebookId == "3524069834274533";
	private Color FacebookColor => IsFacebookIdNullOrDefault ? Color.red : Color.green;
	
	private void CheckFacebookSettings()
	{
		FacebookId = FacebookSettings.AppId;
	}

	[Button(FACEBOOK_SETTINGS), BoxGroup(FACEBOOK_SETTINGS), ShowIf(nameof(IsFacebookIdNullOrDefault))]
	private void OpenFacebookSettings()
	{
		var t = FindAssetWithFilterSearch<FacebookSettings>("t:ScriptableObject FacebookSettings");
		Selection.activeObject = t;
	}
	#endregion

	#region Elephant Check
	[InfoBox(ELEPHANT_GAME_ID_ERROR_MESSAGE, InfoMessageType.Error, VisibleIf = nameof(IsElephantIdNull))]
	[SerializeField, BoxGroup(ELEPHANT_SETTINGS), LabelText(""), GUIColor(nameof(ElephantIDColor))]
	private string ElephantGameID;
	[InfoBox(ELEPHANT_GAME_SECRET_ERROR_MESSAGE, InfoMessageType.Error, VisibleIf = nameof(IsElephantSecretNull))]
	[SerializeField, BoxGroup(ELEPHANT_SETTINGS), LabelText(""), GUIColor(nameof(ElephantSecretColor))]
	private string ElephantGameSecret;

	private const string ELEPHANT_SETTINGS = "Elephant Settings";
	private const string ELEPHANT_GAME_ID_ERROR_MESSAGE = "Elephant Game Id is empty, please check Elephant Settings";
	private const string ELEPHANT_GAME_SECRET_ERROR_MESSAGE = "Elephant Game Secret is empty, please check Elephant Settings";
	private bool IsElephantIdNull => string.IsNullOrEmpty(ElephantGameID);
	private bool IsElephantSecretNull => string.IsNullOrEmpty(ElephantGameSecret);
	private bool IsElephantIdOrSecretNull => IsElephantIdNull || IsElephantSecretNull;
	private Color ElephantIDColor => IsElephantIdNull ? Color.red : Color.green;
	private Color ElephantSecretColor => IsElephantSecretNull ? Color.red : Color.green;

	private void CheckElephantSettings()
	{
		var elephant = FindAssetWithFilterSearch<ElephantSettings>("t:ScriptableObject ElephantSettings");
		if (elephant == null)
		{
			Debug.LogError("Elephant Settings can not be found!");
			return;
		}
		elephant.GameID = elephant.GameID.Trim();
		elephant.GameSecret = elephant.GameSecret.Trim();
		ElephantGameID = elephant.GameID;
		ElephantGameSecret = elephant.GameSecret;
	}

	[Button(ELEPHANT_SETTINGS), BoxGroup(ELEPHANT_SETTINGS), ShowIf(nameof(IsElephantIdOrSecretNull))]
	private void OpenElephantSettings()
	{
		var elephant = FindAssetWithFilterSearch<ElephantSettings>("t:ScriptableObject ElephantSettings");
		Selection.activeObject = elephant;
	}
	#endregion

	#region GameAnalytics Check
	[InfoBox(GAME_ANALYTICS_ERROR_MESSAGE, InfoMessageType.Error, VisibleIf = "IsGameAnalyticsGameKeyDefault")]
	[SerializeField, BoxGroup(GAME_ANALYTICS_SETTINGS), LabelText(""), GUIColor(nameof(GameAnalyticsColor))]
	private string GameAnalyticsGameKey;
	
	private const string GAME_ANALYTICS_SETTINGS = "Game Analytics Settings";
	private const string GAME_ANALYTICS_ERROR_MESSAGE = "GameAnalytics Game Key is set to default template value, please check GameAnalytics Settings";
	private bool IsGameAnalyticsGameKeyDefault => GameAnalyticsGameKey == "51918ad63d5be8731e171a360829ccac";
	public Color GameAnalyticsColor => IsGameAnalyticsGameKeyDefault ? Color.red : Color.green;

	private void CheckGameAnalyticsSettings()
	{
		Settings settings = FindAssetWithFilterSearch<Settings>("t:ScriptableObject Settings");
		GameAnalyticsGameKey = settings.GetGameKey(0);
	}

	[Button(GAME_ANALYTICS_SETTINGS), BoxGroup(GAME_ANALYTICS_SETTINGS), ShowIf(nameof(IsGameAnalyticsGameKeyDefault))]
	private void OpenGameAnalyticsSettings()
	{
		var t = FindAssetWithFilterSearch<Settings>("t:ScriptableObject Settings");
		Selection.activeObject = t;
	}
	#endregion

	#region Target API Level Check
	[InfoBox(ANDROID_API_LEVEL_ERROR_MESSAGE, VisibleIf = nameof(IsTargetAPIAutomaticallyChanged))]
	[SerializeField, BoxGroup(ANDROID_API_LEVEL), LabelText(""), GUIColor(nameof(AndroidTargetAPIColor))]
	private string TargetAPILevel;

	private const string ANDROID_API_LEVEL = "Android API Level";
	private const string ANDROID_API_LEVEL_ERROR_MESSAGE = "Target API Level is automatically set to 29";
	private readonly int androidTargetSDKVersion = 29;
	private bool IsTargetAPIAutomaticallyChanged;
	private Color AndroidTargetAPIColor = Color.green;

	private void CheckTargetAPILevel()
	{
		if ((int)PlayerSettings.Android.targetSdkVersion != androidTargetSDKVersion)
		{
			AndroidSdkVersions androidSdkVersions = (AndroidSdkVersions) Enum.Parse(typeof(AndroidSdkVersions), androidTargetSDKVersion.ToString());
			PlayerSettings.Android.targetSdkVersion = androidSdkVersions;
			IsTargetAPIAutomaticallyChanged = true;
		}
		TargetAPILevel = PlayerSettings.Android.targetSdkVersion.ToString();
	}
	#endregion

	#region Package Name Check
	[InfoBox(PACKAGE_NAME_ERROR_MESSAGE, InfoMessageType.Error, VisibleIf = nameof(IsPackageNameInvalid))]
	[SerializeField, BoxGroup(PACKAGE_NAME), LabelText(""), GUIColor(nameof(PackageNameColor))]
	private string PackageName;

	private const string PACKAGE_NAME_ERROR_MESSAGE = "Package name is invalid. Please assign a new package name!";
	private const string PACKAGE_NAME = "Package Name";
	private const string PLAYER_SETTINGS = "Player Settings";
	private const string OMNIO_TEMPLATE = "omniotemplate";
	private bool IsPackageNameInvalid => PackageName.Contains(OMNIO_TEMPLATE);
	private Color PackageNameColor => IsPackageNameInvalid ? Color.red : Color.green;

	private void CheckPackageName()
	{
		PackageName = Application.identifier;
	}

	[Button(PLAYER_SETTINGS), BoxGroup(PACKAGE_NAME), ShowIf(nameof(IsPackageNameInvalid))]
	private void OpenPlayerSettings()
	{
		SettingsService.OpenProjectSettings("Project/Player");
	}
	#endregion

	#region Unity Splash Check
	[InfoBox(UNITY_SPLASH_ERROR_MESSAGE, VisibleIf = nameof(ShowUnitySplashStateChanged))]
	[SerializeField, BoxGroup(UNITY_SPLASH), LabelText(""), GUIColor(nameof(ShowUnitySplashColor))]
	private string ShowUnitySplashState;

	private const string UNITY_SPLASH_ERROR_MESSAGE = "Unity Splash is automatically disabled";
	private const string UNITY_SPLASH = "Unity Splash";
	private bool ShowUnitySplashStateChanged;
	private Color ShowUnitySplashColor = Color.green;

	private void CheckUnitySplash()
	{
		if (PlayerSettings.SplashScreen.show)
		{
			PlayerSettings.SplashScreen.show = false;
			ShowUnitySplashStateChanged = true;
		}
		ShowUnitySplashState = "Hidden";
	}
	#endregion

	#region Frame Rate Check
	[InfoBox(FRAME_RATE_ERROR_MESSSAGE, InfoMessageType.Error, VisibleIf = nameof(IsFrameRateSetToVideo))]
	[SerializeField, BoxGroup(FRAME_RATE_SETTINGS), LabelText(""), GUIColor(nameof(FrameRateColor))]
	private string FrameRateStatus;

	private const string FRAME_RATE_ERROR_MESSSAGE = "Please Check Frame Rate Status Message For Error Type!";
	private const string FRAME_RATE_SETTINGS = "Frame Rate Settings";
	private bool IsFrameRateSetToVideo => !hasControllerHub || setToVideo;
	private bool hasControllerHub;
	private bool setToVideo;
	private Color FrameRateColor => IsFrameRateSetToVideo ? Color.red : Color.green;

	private void CheckFrameRate()
	{
		Object[] hubs = Resources.FindObjectsOfTypeAll(typeof(ControllerHub));
		if (hubs.Length <= 0)
		{
			hasControllerHub = false;
			FrameRateStatus = "No controller hub in the scene!";
			return;
		}
		hasControllerHub = true;
		ControllerHub hub = hubs[0].GetComponent<ControllerHub>();
		setToVideo = hub.videoFrameRate;
		FrameRateStatus = setToVideo ? "30 FPS" : "60 FPS";
	}
	#endregion
	
	#region Adjust Check
	// [InfoBox("Main Scene should be opened before checking the adjust state", InfoMessageType.Error, VisibleIf = "_shouldOpenFirstScene")]
	// [InfoBox("$ActiveBuildTargetMessage", VisibleIf = "ShowSelectAnalyticsImporterButton")]
	// [GUIColor("AdjustStateColor")]
	// [BoxGroup("Adjust")]
	// public string AdjustState;
	// private bool _shouldOpenFirstScene;
	// private bool _isAdjustIdNull;
	// public bool ShowSelectAnalyticsImporterButton => !_shouldOpenFirstScene && _isAdjustIdNull;
	// private AnalyticsImporter _analyticsImporter;
	// public string ActiveBuildTargetMessage => (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android ? "Android" : EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS ? "iOS" : "Unidentified") + " Token is null";
	//
	// public Color AdjustStateColor => (_shouldOpenFirstScene || _isAdjustIdNull) ? Color.red : Color.green;
	// private void CheckAdjustSettings()
	// {
	// 	if (SceneManager.GetActiveScene().buildIndex != 0)
	// 	{
	// 		_shouldOpenFirstScene = true;
	// 		AdjustState = "Unchecked";
	// 		return;
	// 	}
	//
	// 	_analyticsImporter = FindObjectOfType<AnalyticsImporter>();
	// 	if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
	// 	{
	// 		_isAdjustIdNull = string.IsNullOrEmpty(_analyticsImporter.AndroidToken);
	// 		AdjustState = _isAdjustIdNull ? "Null" : _analyticsImporter.AndroidToken;
	// 	}
	// 	else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS)
	// 	{
	// 		_isAdjustIdNull = string.IsNullOrEmpty(_analyticsImporter.IOSToken);
	// 		AdjustState = _isAdjustIdNull ? "Null" : _analyticsImporter.IOSToken;
	// 	}
	// 	
	// }
	// [BoxGroup("Adjust")]
	// [ShowIf("_shouldOpenFirstScene")]
	// [Button("Open Scene")]
	// private void OpenFirstScene()
	// {
	// 	EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
	// 	EditorSceneManager.OpenScene("Assets/_GAME_/Scenes/Main Scene.unity");
	// 	_shouldOpenFirstScene = false;
	// 	CheckAdjustSettings();
	// }
	// [ShowIf("ShowSelectAnalyticsImporterButton")]
	// [BoxGroup("Adjust")]
	// [Button("AnalyticsImporter")]
	// private void SelectAnalyticsImporter()
	// {
	// 	Selection.activeObject = _analyticsImporter;
	// }
	#endregion

	#region Build App Bundle Check
	[InfoBox(BUILD_APP_BUNDLE_ERROR_MESSAGE, InfoMessageType.Error, VisibleIf = nameof(BuildAppBundleSet))]
	[SerializeField, BoxGroup(BUILD_APP_BUNDLE), LabelText(""), GUIColor(nameof(BuildAppBundleColor))]
	private string BuildAppBundleState;

	private const string BUILD_APP_BUNDLE_ERROR_MESSAGE = "Build App Bundle is automatically set to true";
	private const string BUILD_APP_BUNDLE = "Build App Bundle";
	private bool BuildAppBundleSet;
	private Color BuildAppBundleColor = Color.green;

	private void CheckBuildAppBundle()
	{
		if (!EditorUserBuildSettings.buildAppBundle)
		{
			EditorUserBuildSettings.buildAppBundle = true;
			BuildAppBundleSet = true;
		}
		BuildAppBundleState = "true";
	}
	#endregion

	#region Debug Mode Check
	[InfoBox(DEBUG_MODE_ERROR_MESSAGE, InfoMessageType.Error, VisibleIf = nameof(ShowDebugModeWarning))]
	[SerializeField, BoxGroup(DEBUG_MODE_SETTINGS), LabelText(""), GUIColor(nameof(DebugModeColor))]
	private string DebugModeStatus;

	private const string DEBUG_MODE_ERROR_MESSAGE = "Please Check Debug Mode Status!";
	private const string DEBUG_MODE_SETTINGS = "Debug Mode Settings";
	private bool ShowDebugModeWarning => !hasLevelController || isDebugModeActive;
	private Color DebugModeColor => hasLevelController && !isDebugModeActive ? Color.green : Color.red;
	private bool hasLevelController;
	private bool isDebugModeActive;

	private void CheckDebugMode()
	{
		Object[] levelControllers = Resources.FindObjectsOfTypeAll(typeof(LevelController));
		if (levelControllers.Length <= 0)
		{
			hasLevelController = false;
			DebugModeStatus = "No level controller in the scene!";
			return;
		}
		hasLevelController = true;
		LevelController levelController = levelControllers[0].GetComponent<LevelController>();
		isDebugModeActive = levelController.DebugMode;
		DebugModeStatus = "Debug mode is " + (levelController.DebugMode ? "activated!" : "not activated!");
	}
	#endregion
	
	private T FindAssetWithFilterSearch<T>(string filter) where T : Object
	{
		var assetGuids = AssetDatabase.FindAssets(filter);
		List<string> assetPaths = new List<string>();
		assetGuids.ForEach(s => assetPaths.Add(AssetDatabase.GUIDToAssetPath(s)));
		var found = assetPaths.Find(s => AssetDatabase.LoadAssetAtPath<T>(s) != null);
		return string.IsNullOrEmpty(found) ? null : AssetDatabase.LoadAssetAtPath<T>(found);
	}
	
}