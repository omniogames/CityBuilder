using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace _GAME_.Scripts.EventSystem.Editor
{
	public class TemplateGenerator : EditorWindow
	{
		private string _eventName;
		private DeclarationType _type = DeclarationType.Struct;
		
		[MenuItem("OmnioCore/Generate Game Event...")]
		private static void ShowWindow()
		{
			var window = GetWindow<TemplateGenerator>();
			window.titleContent = new GUIContent("Game Event Generator");
			window.Show();
		}

		private void OnGUI()
		{
			_eventName = EditorGUILayout.TextField("Event Name", _eventName);
			_type = (DeclarationType)EditorGUILayout.EnumPopup("Type", _type);
			if (GUILayout.Button("Create"))
			{
				GenerateTemplateEvent("Data", _type, _eventName);
				GenerateTemplateEvent("GameEvent", _type, _eventName);
				GenerateTemplateEvent("GameEventListener", _type, _eventName);
				GenerateTemplateEvent("UnityGameEvent", _type, _eventName);
			}
		}

		private void GenerateTemplateEvent(string suffix, DeclarationType type, string eventName)
		{
			string decType = type == DeclarationType.Class ? "class" : "struct";
			
			string dataPath = Application.dataPath;
			string projectDirPath = Directory.GetParent(dataPath).FullName;
			projectDirPath = Path.Combine(projectDirPath, "Assets");
			string gameFolderDirPath = Path.Combine(projectDirPath, "_GAME_");
			string scriptsFolderDirPath = Path.Combine(gameFolderDirPath, "Scripts");
			string eventSystemFolderDirPath = Path.Combine(scriptsFolderDirPath, "EventSystem");
			string templateFolderDirPath = Path.Combine(eventSystemFolderDirPath, "Template");
			string templateDirPath = Path.Combine(templateFolderDirPath, "EVENTNAME" + suffix) + ".cs";
			string template = File.ReadAllText(templateDirPath);

			string result = template
				.Replace("TYPE", decType)
				.Replace("EVENTNAME", eventName);

			string gameEventsFolderPath = Path.Combine(eventSystemFolderDirPath, "_GameEvents");
			string eventFolderPath = Path.Combine(gameEventsFolderPath, eventName);
			if (!Directory.Exists(eventFolderPath))
			{
				Directory.CreateDirectory(eventFolderPath);
			}

			eventFolderPath = Path.Combine(eventFolderPath, eventName + suffix) + ".cs";
			File.WriteAllText(eventFolderPath, result);
			AssetDatabase.Refresh();
		}
	}

	public enum DeclarationType
	{
		Struct,
		Class
	}
	
	
}