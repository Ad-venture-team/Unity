using System;

[Serializable]
public abstract class MonsterActionCondition
{
    public abstract float Evaluate(Monster _monster);

    public virtual void OnDoAction() { }
}
