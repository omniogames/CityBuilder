using System.Collections;
using MoreMountains.NiceVibrations;
using UnityEngine;

public class HapticManager : BaseController
{

    [SerializeField] private bool enableEditorOnlyTrigger = true;

    public bool IsHapticEnabled => PlayerPrefs.GetInt(Consts.PrefKeys.HAPTIC, 1) == 1;

    private void OnEnable()
    {
        EventManager.LevelSuccessEvent.AddListener(PlaySuccessHaptic);
        EventManager.LevelFailEvent.AddListener(PlayFailHaptic);
    }

    private void OnDisable()
    {
        EventManager.LevelSuccessEvent.RemoveListener(PlaySuccessHaptic);
        EventManager.LevelFailEvent.RemoveListener(PlayFailHaptic);
    }

    public void PlaySuccessHaptic()
    {
        PlayHaptic(HapticTypes.Success);
    }

    public void PlayFailHaptic()
    {
        PlayCoroutine(HapticTypes.HeavyImpact, 2, .2f);
    }

    public void SelectionHaptic()
    {
        PlayHaptic(HapticTypes.Selection);
    }
    
    #region Haptics
    public void PlayCoroutineHaptic(HapticTypes type, int count, float delayTime)
    {
        if(!IsHapticEnabled) return;
        StartCoroutine(PlayCoroutine(type, count, delayTime));
    }

    private IEnumerator PlayCoroutine(HapticTypes type, int count, float delayTime)
    {
        for (var i = 0; i < count; i++)
        {
            PlayHaptic(type);
            yield return new WaitForSeconds(delayTime);
        }
    }

    public void PlayCoroutineHaptic(HapticTypes type, float time, float delayTime)
    {
        if(!IsHapticEnabled) return;

        StartCoroutine(PlayCoroutine(type, time, delayTime));
    }

    private IEnumerator PlayCoroutine(HapticTypes type, float time, float delayTime)
    {
        float startTime = Time.time;
        while (Time.time - startTime < time)
        {
            PlayHaptic(type);
            yield return new WaitForSeconds(delayTime);
        }
    }

    public void PlayHaptic(HapticTypes type)
    {
        if (!IsHapticEnabled) return;
        MMVibrationManager.Haptic(type);
#if UNITY_EDITOR
        if (enableEditorOnlyTrigger)
        {
            Debug.Log("*EDITORONLY* Triggered Haptic " + type);
        }
#endif
    }
    #endregion


}