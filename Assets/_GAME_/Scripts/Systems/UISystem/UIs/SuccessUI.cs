using UnityEngine;

public class SuccessUI : BaseUI
{
	
	private void OnEnable()
	{
		EventManager.LevelLoadedEvent.AddListener(OnLevelLoaded);
		EventManager.LevelSuccessEvent.AddListener(OnLevelSuccess);
	}

	private void OnDisable()
	{
		EventManager.LevelLoadedEvent.RemoveListener(OnLevelLoaded);
		EventManager.LevelSuccessEvent.RemoveListener(OnLevelSuccess);
	}

	private void OnLevelLoaded(LevelLoadedEventData eventData)
	{
		SetHidden();
	}

	private void OnLevelSuccess()
	{
		SetShown();
	}
	
}
