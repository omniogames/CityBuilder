
[System.Serializable]
public class LevelLoadedEventData
{
    public LevelContent LevelContent;
    public LevelFacade LevelFacade;
    public int LevelNo;

    public LevelLoadedEventData(LevelContent levelContent, LevelFacade levelFacade, int levelNo)
    {
        LevelContent = levelContent;
        LevelFacade = levelFacade;
        LevelNo = levelNo;
    }
}
