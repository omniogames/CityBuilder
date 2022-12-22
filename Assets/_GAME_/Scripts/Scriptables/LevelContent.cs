using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelContent", menuName = "Settings/LevelContent")]
public class LevelContent : ScriptableObject
{
	public LevelFacade LevelFacade;
	
	[InlineEditor]
	public LightingSettings LightingSettings;
}

