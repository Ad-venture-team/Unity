
public abstract class AState<T>
{
    public AStateManager<T> owner;

    public AState (AStateManager<T> _owner)
    {
        owner = _owner;
    }

    public virtual void EnterState() { }
    public virtual void UpdateState() 
    { 
        CheckStateChange();
    }
    public virtual void ExitState() { }
    public abstract void CheckStateChange();
}
