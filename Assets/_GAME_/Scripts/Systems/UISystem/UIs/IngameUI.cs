using TMPro;
using UnityEngine;

public class IngameUI : BaseUI
{
	[SerializeField] private TextMeshProUGUI levelText;

	private void OnEnable()
	{
		EventManager.LevelLoadedEvent.AddListener(OnLevelLoaded);
		EventManager.LevelSuccessEvent.AddListener(OnLevelSuccess);
		EventManager.LevelFailEvent.AddListener(OnLevelFail);
	}

	private void OnDisable()
	{
		EventManager.LevelLoadedEvent.RemoveListener(OnLevelLoaded);
		EventManager.LevelSuccessEvent.RemoveListener(OnLevelSuccess);
		EventManager.LevelFailEvent.RemoveListener(OnLevelFail);
	}

	private void OnLevelLoaded(LevelLoadedEventData eventData)
	{
		levelText.text = "LEVEL " +eventData.LevelNo.ToString("0");
		SetShown();
	}

	private void OnLevelFail()
	{
		SetHidden();
	}

	private void OnLevelSuccess()
	{
		SetHidden();
	}
	
}
