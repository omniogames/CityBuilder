using System;
using System.Collections.Generic;
using OmnioCore.Singleton;
using Sirenix.OdinInspector;
using UnityEngine;

public class ControllerHub : PersistentSingleton<ControllerHub>
{

    [SerializeField]
    private List<BaseController> controllersPriorityList;

    public bool videoFrameRate;

    private readonly Dictionary<Type, BaseController> controllers = new Dictionary<Type, BaseController>();

    private void Start()
    {
        Application.targetFrameRate = videoFrameRate? 30 : 60;
        PopulateDictionary();
        InitControllers();
    }

    private void PopulateDictionary()
    {
        foreach (BaseController controller in controllersPriorityList)
        {
            controllers.Add(controller.GetType(), controller);
        }
    }

    private void InitControllers()
    {
        foreach (BaseController controller in controllers.Values)
        {
            controller.Init();
        }
        EventManager.ControllersInitializedEvent.Invoke();
    }

    public static T Get<T>() where T : BaseController
    {
        return (T)Instance.controllers[typeof(T)];
    }

#if UNITY_EDITOR
    [Button(ButtonSizes.Medium)]
    public void FindControllers()
    {
        foreach (BaseController controller in FindObjectsOfType<BaseController>())
        {
            if (!controllersPriorityList.Exists(x => x.GetType() == controller.GetType()))
            {
                controllersPriorityList.Add(controller);
            }
            else
            {
                Debug.LogWarning($"An object with type {controller.GetType()} already exists in list. " +
                    $"There can only be one of each type.");
            }
        }
    }
#endif

}
