using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class BaseGameEventListener<T, TGameEvent, TUnityEvent> : MonoBehaviour where TGameEvent : BaseGameEvent<T> where TUnityEvent : UnityEvent<T>
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
	[SerializeField] private TGameEvent _event;
	[BoxGroup("Event Settings")]
	[Min(0)]
	[SerializeField] private float _delay;
	
	public TUnityEvent Response;

	private void OnEnable()
	{
		_event.AddListener(OnEventRaised);
	}

	private void OnDisable()
	{
		_event.RemoveListener(OnEventRaised);
	}

	private void OnEventRaised(T t)
	{
		if (_delay == 0)
		{
			Response.Invoke(t);
		}
		else
		{
			StartCoroutine(LateEventRaised(t));
		}
	}

	private IEnumerator LateEventRaised(T t)
	{
		yield return new WaitForSeconds(_delay);
		Response.Invoke(t);
	}
}