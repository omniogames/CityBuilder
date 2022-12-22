using UnityEngine;

public class LevelController : BaseLevelController
{
	protected override void LoadLevel()
	{
		base.LoadLevel();
		PrepareLevel();
	}
	
	private void PrepareLevel()
	{
		LevelFacade levelFacade = InstantiateAsDestroyable<LevelFacade>(LevelContent.LevelFacade);

		//this is the place where you should add your in-game logic such as instantiating player etc.
		Transform target = null;
		if (target != null)
		{
			ControllerHub.Get<CameraManager>().Init(target);
		}
		SendLevelLoadedEvent(levelFacade);
	}

}