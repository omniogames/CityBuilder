using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Game Event", menuName = "OmnioCore/Events/GameEvent")]
public class GameEvent : ScriptableObject
{
	#region ODIN INSPECTOR
#if UNITY_EDITOR
	[OnInspectorGUI, HideInPlayMode]
	private void OnInspectorGUI()
	{
		UnityEditor.EditorGUILayout.HelpBox("Start playing to invoke the event from here", UnityEditor.MessageType.Info);
	}
#endif

	#endregion

	private Action _action = delegate {  }; 
	
	[Button("Invoke"), HideInEditorMode, GUIColor(0, 1, 0)]
	public void Invoke()
	{
		_action();
	}

	public void AddListener(Action item)
	{
		_action += item;
	}	
	public void RemoveListener(Action item)
	{
		_action -= item;
	}	
}

[System.Serializable]
public class UnityGameEvent : UnityEvent {}
