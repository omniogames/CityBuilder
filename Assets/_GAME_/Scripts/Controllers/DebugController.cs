using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class DebugController : MonoBehaviour
{
	[SerializeField] private GameObject _levelButtonPrefab;
	[SerializeField] private Button _screenButton;
	[SerializeField] private Transform _debugScreen;
	[SerializeField] private Transform _levelParent;
	[SerializeField] private TextMeshProUGUI _timerText;
	private float _levelStartTime;
	private bool _shouldUpdateTime;
	
	private void OnEnable()
	{
		EventManager.LevelStartEvent.AddListener(OnLevelStart);
		EventManager.LevelFailEvent.AddListener(OnLevelFail);
		EventManager.LevelSuccessEvent.AddListener(OnLevelSuccess);
	}

	private void OnDisable()
	{
		EventManager.LevelStartEvent.RemoveListener(OnLevelStart);
		EventManager.LevelFailEvent.RemoveListener(OnLevelFail);
		EventManager.LevelSuccessEvent.RemoveListener(OnLevelSuccess);
	}
	private void OnLevelSuccess()
	{
		StopTimer();
	}

	private void OnLevelFail()
	{
		StopTimer();
	}

	private void OnLevelStart()
	{
		StartTimer();
	}

	private void Update()
	{
		if (_shouldUpdateTime)
		{
			TimeSpan span = TimeSpan.FromSeconds(Time.realtimeSinceStartup - _levelStartTime);
			_timerText.text = span.ToString(@"mm\:ss");
		}
	}

	private void StartTimer()
	{
		_shouldUpdateTime = true;
		_levelStartTime = Time.realtimeSinceStartup;
		_timerText.color = Color.white;
	}

	private void StopTimer()
	{
		_shouldUpdateTime = false;
		_timerText.color = Color.red;
	}

	public void LoadLevels(LevelContent[] levels)
	{
		
	}
}