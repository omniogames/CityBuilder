using System.Collections.Generic;
using System.Linq;
using ElephantSDK;
using GameAnalyticsSDK;
using UnityEngine;

public class AnalyticsController : BaseController
{
    private int _levelIndex;

    private void OnEnable()
    {
        EventManager.LevelLoadedEvent.AddListener(OnLevelLoaded);
        EventManager.LevelStartEvent.AddListener(OnLevelStart);
        EventManager.LevelSuccessEvent.AddListener(OnLevelSuccess);
        EventManager.LevelFailEvent.AddListener(OnLevelFail);
    }

    private void OnDisable()
    {
        EventManager.LevelLoadedEvent.RemoveListener(OnLevelLoaded);
        EventManager.LevelStartEvent.RemoveListener(OnLevelStart);
        EventManager.LevelSuccessEvent.RemoveListener(OnLevelSuccess);
        EventManager.LevelFailEvent.RemoveListener(OnLevelFail);
    }

    private void OnLevelLoaded(LevelLoadedEventData eventData)
    {
        _levelIndex = eventData.LevelNo;
    }

    private void OnLevelStart()
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            {Consts.AnalyticsEventNames.LEVEL_START, _levelIndex}
        };
#if UNITY_EDITOR
        Debug.Log(
            $"*EDITOR_ONLY*\nSending Event : {Consts.AnalyticsEventNames.LEVEL_START} --- Parameters : {parameters.ToDebugString()}");
        return;
#endif
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, _levelIndex.ToString());
        Elephant.LevelStarted(_levelIndex);
    }

    private void OnLevelSuccess()
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            {Consts.AnalyticsEventNames.LEVEL_SUCCESS, _levelIndex}
        };
#if UNITY_EDITOR
        Debug.Log(
            $"*EDITOR_ONLY*\nSending Event : {Consts.AnalyticsEventNames.LEVEL_SUCCESS} --- Parameters : {parameters.ToDebugString()}");
        return;
#endif
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, _levelIndex.ToString());
        Elephant.LevelCompleted(_levelIndex);
    }

    private void OnLevelFail()
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            {Consts.AnalyticsEventNames.LEVEL_FAIL, _levelIndex}
        };
#if UNITY_EDITOR
        Debug.Log(
            $"*EDITOR_ONLY*\nSending Event : {Consts.AnalyticsEventNames.LEVEL_FAIL} --- Parameters : {parameters.ToDebugString()}");
        return;
#endif
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, _levelIndex.ToString());
        Elephant.LevelFailed(_levelIndex);
    }

    private void SendElephantEvent(string eventName, Dictionary<string, object> parameters)
    {
        int level = (int) parameters[Consts.AnalyticsDataName.LEVEL];
        Params elephantParameters = Params.New();
        foreach (KeyValuePair<string, object> parameter in parameters.Where(parameter =>
            !parameter.Key.Equals(Consts.AnalyticsDataName.LEVEL)))
        {
            switch (parameter.Value)
            {
                case int intValue:
                    elephantParameters.Set(parameter.Key, intValue);
                    break;
                case float floatValue:
                    elephantParameters.Set(parameter.Key, floatValue);
                    break;
                case string stringValue:
                    elephantParameters.Set(parameter.Key, stringValue);
                    break;
                default:
                    Debug.LogError($"Parameter type({parameter.Value.GetType()}) is not supported by Elephant!");
                    break;
            }
        }

        Elephant.Event(eventName, level, elephantParameters);
    }

    public void SendAnalyticEvents(string eventName)
    {
        var parameters = new Dictionary<string, object>
        {
            [Consts.AnalyticsDataName.LEVEL] = _levelIndex,
        };
#if UNITY_EDITOR
        Debug.Log(
            $"*EDITORONLY* Sending GameAnalytics Event : {eventName} --- Parameters : {parameters.ToDebugString()}");
        return;
#endif
        GameAnalytics.NewDesignEvent(eventName, _levelIndex);
        SendElephantEvent(eventName, parameters);
    }
}