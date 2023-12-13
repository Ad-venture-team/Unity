using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AStateManager<T>
{
    public T item;
    public AState<T> currentState;

    public AStateManager(T _item)
    {
        item = _item;
    }

    public void ChangeState(AState<T> _newState)
    {
        currentState?.ExitState();
        currentState = _newState;
        currentState.EnterState();
    }

    public void UpdateState()
    {
        currentState?.UpdateState();
    }
}
