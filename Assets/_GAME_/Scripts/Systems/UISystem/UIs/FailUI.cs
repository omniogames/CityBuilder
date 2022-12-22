using UnityEngine;

public class FailUI : BaseUI
{
	
	private void OnEnable()
	{
		EventManager.LevelLoadedEvent.AddListener(OnLevelLoaded);
		EventManager.LevelFailEvent.AddListener(OnLevelFail);
	}

	private void OnDisable()
	{
		EventManager.LevelLoadedEvent.RemoveListener(OnLevelLoaded);
		EventManager.LevelFailEvent.RemoveListener(OnLevelFail);
	}

	private void OnLevelLoaded(LevelLoadedEventData eventData)
	{
		SetHidden();
	}

	private void OnLevelFail()
	{
		SetShown();
	}
	
}
