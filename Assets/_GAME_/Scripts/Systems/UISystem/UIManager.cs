using System;
using System.Collections.Generic;
using OmnioCore.Singleton;
using Sirenix.OdinInspector;
using UnityEngine;

public class UIManager : PersistentSingleton<UIManager>
{
    
    [SerializeField, LabelText("UI Priority List")]
    private List<BaseUI> uiPriorityList;

    private readonly Dictionary<Type, BaseUI> uis = new Dictionary<Type, BaseUI>();

    protected override void Awake()
    {
        base.Awake();
        PopulateDictionary();
    }

    private void PopulateDictionary()
    {
        foreach (BaseUI ui in uiPriorityList)
        {
            uis.Add(ui.GetType(), ui);
        }
    }

    public static T Get<T>() where T : BaseUI
    {
        return (T) Instance.uis[typeof(T)];
    }

#if UNITY_EDITOR
    [Button(ButtonSizes.Medium)]
    public void FindControllers()
    {
        foreach (BaseUI ui in FindObjectsOfType<BaseUI>())
        {
            if (!uiPriorityList.Exists(x => x.GetType() == ui.GetType()))
            {
                uiPriorityList.Add(ui);
            }
            else
            {
                Debug.LogWarning($"An object with type {ui.GetType()} already exists in list. " +
                    $"There can only be one of each type.");
            }
        }
    }
#endif
    
}
