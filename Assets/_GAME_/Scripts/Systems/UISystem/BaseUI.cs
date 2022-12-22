using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public abstract class BaseUI : MonoBehaviour
{

    [SerializeField]
    protected RectTransform _parentPanel;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private UIActionHook _hook;
    
    public Action<UIState> OnStateChanged;
    protected UIState _currentState;
    private int param_Hiding = Animator.StringToHash("Hiding");
    private int param_Showing = Animator.StringToHash("Showing");
    
    public UIState CurrentState
    {
        get => _currentState;
        private set
        {
            var oldState = _currentState;
            _currentState = value;
            if (oldState != _currentState)
            {
                OnStateChanged?.Invoke(_currentState);
                Debug.Log(_parentPanel.name + " " + _currentState);
                InvokeActionHooks(_currentState);
            }
        }
    }
    
    protected virtual void Awake()
    {
        gameObject.SetActive(true);
        Assert.IsTrue(_parentPanel != null, "No parent object found!");
    }

    private void InvokeActionHooks(UIState state)
    {
        switch (state)
        {
            case UIState.Showing:
                _hook.OnShowing?.Invoke();
                break;
            case UIState.Shown:
                _hook.OnShown?.Invoke();
                break;
            case UIState.Hiding:
                _hook.OnHiding?.Invoke();
                break;
            case UIState.Hidden:
                _hook.OnHidden?.Invoke();
                break;
        }
    }

    public void Show()
    {
        CurrentState = UIState.Showing;
        _parentPanel.gameObject.SetActive(true);
        if (_animator != null)
        {
            _animator.SetTrigger(param_Showing);
        }
        OnShowing();
    }

    public void Hide()
    {
        CurrentState = UIState.Hiding;
        if (_animator != null)
        {
            _animator.SetTrigger(param_Hiding);
        }
        OnHiding();
    }

    public void SetShown()
    {
        _parentPanel.gameObject.SetActive(true);
        CurrentState = UIState.Shown;

        OnShown();
    }

    public void SetHidden()
    {
        CurrentState = UIState.Hidden;
        _parentPanel.gameObject.SetActive(false);
        OnHidden();
    }

    protected virtual void OnShowing()
    {

    }
    protected virtual void OnShown()
    {

    }
    protected virtual void OnHiding()
    {

    }
    protected virtual void OnHidden()
    {

    }
}

public enum UIState
{
    Default,
    Showing,
    Shown,
    Hiding,
    Hidden
}

[System.Serializable]
public class UIActionHook
{
    public UnityEvent OnShowing;
    public UnityEvent OnShown;
    public UnityEvent OnHiding;
    public UnityEvent OnHidden;
}
