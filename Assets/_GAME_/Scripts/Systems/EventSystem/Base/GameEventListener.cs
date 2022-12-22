using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameEventListener : MonoBehaviour
{
	[InfoBox("Adding/Removing new events on runtime won't work", VisibleIf = "IsInPlayMode")]
	[SerializeField] private List<GameEventWrapper> _gameEvents;

	public bool IsInPlayMode => Application.isPlaying;
	private void Awake()
	{
		for (int i = 0; i < _gameEvents.Count; i++)
		{
			_gameEvents[i].Initialize(this);
		}
	}

	private void OnEnable()
	{
		for (int i = 0; i < _gameEvents.Count; i++)
		{
			_gameEvents[i].AddListener();
		}
	}

	private void OnDisable()
	{
		for (int i = 0; i < _gameEvents.Count; i++)
		{
			_gameEvents[i].RemoveListener();
		}
	}
}

[System.Serializable]
public class GameEventWrapper
{
	#region ODIN INSPECTOR

	private bool _requireRegistering;
#if UNITY_EDITOR
	private bool _changedOnRuntime;
	private void OnEventValueChanged()
	{
		if (_requireRegistering)
		{
			_event.AddListener(OnEventRaised);
			_requireRegistering = false;
		}
		else
		{
			if (Application.isPlaying)
			{
				_changedOnRuntime = true;
			}
		}
	}
#endif	

	

	#endregion	
	[InlineEditor()]
	[BoxGroup("Event Settings")]
	[InfoBox("Changing events on runtime might cause unexpected behaviour.", InfoMessageType.Warning, VisibleIf = "_changedOnRuntime")]
	[OnValueChanged("OnEventValueChanged")]
	[SerializeField] private GameEvent _event;
	[BoxGroup("Event Settings")]
	[Min(0)]
	[SerializeField] private float _delay;

	public UnityGameEvent Response;

	private GameEventListener _listener;

	public void Initialize(GameEventListener listener)
	{
		_listener = listener;
	}

	public void AddListener()
	{
		if (_event == null)
		{
			return;
		}

		_event.AddListener(OnEventRaised);
	}

	public void RemoveListener()
	{
		if (_event == null)
		{
			return;
		}

		_event.RemoveListener(OnEventRaised);
	}

	private void OnEventRaised()
	{
		if (_delay == 0)
		{
			Response.Invoke();
		}
		else
		{
			_listener.StartCoroutine(LateEventRaised());
		}
	}

	private IEnumerator LateEventRaised()
	{
		yield return new WaitForSeconds(_delay);
		Response.Invoke();
	}
}