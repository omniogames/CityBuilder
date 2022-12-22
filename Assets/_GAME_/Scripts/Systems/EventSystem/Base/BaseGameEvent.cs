using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BaseGameEvent<T> : ScriptableObject
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

	private Action<T> _action = delegate {  }; 
	
	[Button("Invoke"), HideInEditorMode, GUIColor(0, 1, 0)]
	public void Invoke(T item)
	{
		_action(item);
	}

	public void AddListener(Action<T> item)
	{
		_action += item;
	}	
	public void RemoveListener(Action<T> item)
	{
		_action -= item;
	}	
}