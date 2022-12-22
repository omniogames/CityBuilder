using UnityEngine.Events;

public static class EventManager
{
    #region GameCycleEvents
    public static readonly ControllersInitializedEvent ControllersInitializedEvent = new ControllersInitializedEvent();
    public static readonly LevelLoadedEvent LevelLoadedEvent = new LevelLoadedEvent();
    public static readonly LevelStartEvent LevelStartEvent = new LevelStartEvent();
    public static readonly LevelSuccessEvent LevelSuccessEvent = new LevelSuccessEvent();
    public static readonly LevelFailEvent LevelFailEvent = new LevelFailEvent();
    public static readonly LevelResetEvent LevelResetEvent = new LevelResetEvent();
    #endregion

    #region Input
    public static readonly PointerDownEvent PointerDownEvent = new PointerDownEvent();
    public static readonly PointerUpEvent PointerUpEvent = new PointerUpEvent();
    #endregion

    #region CameraEvents
    public static readonly ShakeCameraEvent ShakeCameraEvent = new ShakeCameraEvent();
    #endregion

}

#region GameCycleEvents
public class ControllersInitializedEvent : UnityEvent {}
public class LevelLoadedEvent : UnityEvent<LevelLoadedEventData> {}
public class LevelStartEvent : UnityEvent {}
public class LevelSuccessEvent : UnityEvent {}
public class LevelFailEvent : UnityEvent {}
public class LevelResetEvent : UnityEvent {}
#endregion

#region Input
public class PointerDownEvent : UnityEvent {}
public class PointerUpEvent : UnityEvent {}
#endregion

#region CameraEvents
public class ShakeCameraEvent : UnityEvent<ShakeCameraData> {}
#endregion
