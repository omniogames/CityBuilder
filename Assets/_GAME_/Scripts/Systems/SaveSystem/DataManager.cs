using Sirenix.OdinInspector;

namespace OmnioCore.Save
{
	
	public class DataManager : BaseController
	{

		public readonly LevelSaveManager LevelSaveManager = new LevelSaveManager();

		public override void Init()
		{
			base.Init();

			LevelSaveManager.Init(Consts.FileNames.LEVELDATA);
		}

		[Button(ButtonSizes.Medium)]
		public void ClearAllData()
		{
			LevelSaveManager.ClearData();
		}

	}

}