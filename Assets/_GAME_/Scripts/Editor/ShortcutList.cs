using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace _GAME_.Scripts.Editor
{
	public class ShortcutList : EditorWindow
	{
		private string _eventName;

		[MenuItem("OmnioCore/Shortcut List")]
		private static void ShowWindow()
		{
			var window = GetWindow<ShortcutList>();
			window.titleContent = new GUIContent("Shortcut List");
			window.Show();
		}

		private void OnGUI()
		{
			ReadOnlyTextField("Copy / Paste Transform Values:","copy: Alt + c, paste: Alt + v");
			ReadOnlyTextField("Multi-Object Rename Wizard:","Alt + f");
			ReadOnlyTextField("Copy / Paste Center Position:","copy: Alt + k, paste: Alt + l");
			ReadOnlyTextField("Duplicate Without Auto-Name:","Alt + d");
			ReadOnlyTextField("Remove Auto-Name:","Alt + r");
			ReadOnlyTextField("Invert Active:","Alt + a");
			ReadOnlyTextField("Deselect All:","Shift + d");
			ReadOnlyTextField("On Mac:","Alt = Option");
		}
		void ReadOnlyTextField(string label, string text)
		{
			EditorGUILayout.BeginHorizontal();
			{
				if(!string.IsNullOrEmpty(label))
					EditorGUILayout.LabelField(label, GUILayout.Height(EditorGUIUtility.singleLineHeight));
				EditorGUILayout.LabelField(text, EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
			}
			EditorGUILayout.EndHorizontal();
		}
	}
}