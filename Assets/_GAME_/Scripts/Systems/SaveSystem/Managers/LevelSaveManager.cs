using System.Collections.Generic;
using OmnioCore.Save.Data;
using Sirenix.OdinInspector;

namespace OmnioCore.Save
{
	[System.Serializable]
	public class LevelSaveManager : BaseJsonSaveManager<LevelSaveData>
	{

		protected override JsonSerializer<LevelSaveData> Serializer
		{
			get
			{
				if (_serializer == null)
				{
					Init(Consts.FileNames.LEVELDATA);
				}
				return _serializer;
			}
		}

		public int CurrentLevelIndex => Serializer.Data.currentLevelIndex;
		public int CurrentLevelNo => Serializer.Data.currentLevelNo;
		public List<int> LevelIndicesToRepeat => Serializer.Data.levelIndicesToRepeat;

		[Button("Set CurrentLevelIndex", ButtonSizes.Medium)]
		public void SetCurrentLevelIndex(int currentLevelIndex, bool save = true)
		{
			Serializer.Data.currentLevelIndex = currentLevelIndex;
			if (save)
			{
				SaveData();
			}
		}

		[Button("Set CurrentLevelNo", ButtonSizes.Medium)]
		public void SetCurrentLevelNo(int currentLevelNo, bool save = true)
		{
			Serializer.Data.currentLevelNo = currentLevelNo;
			if (save)
			{
				SaveData();
			}
		}

		[Button("Set LevelIndicesToRepeat", ButtonSizes.Medium)]
		public void SetLevelIndicesToRepeat(List<int> levelIndicesToRepeat, bool save = true)
		{
			Serializer.Data.levelIndicesToRepeat = levelIndicesToRepeat;
			if (save)
			{
				SaveData();
			}
		}

	}
}
