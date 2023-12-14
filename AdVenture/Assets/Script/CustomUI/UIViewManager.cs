using System;
using UnityEngine;

[RequireComponent(typeof(UIView))]
public abstract class UIViewManager : MonoBehaviour
{
    public UIView view;

    public void OnValidate()
    {
        view = GetComponent<UIView>();
    }

    public virtual void Start()
    {
        view = GetComponent<UIView>();
    }

    public virtual void Show()
    {
        view.Show();
    }

    public virtual void Show(Action onShowEnd = null)
    {
        view.Show(onShowEnd);
    }

    public virtual void Hide()
    {
        view.Hide();
    }

    public virtual void Hide(Action onHideEnd = null)
    {
        view.Hide(onHideEnd);
    }
}
