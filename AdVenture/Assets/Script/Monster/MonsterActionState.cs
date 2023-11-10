using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class MonsterActionState
{
    [SerializeReference] public List<MonsterActionCondition> condition =  new List<MonsterActionCondition>();

    protected Monster owner;
    public float Evaluate(Monster _monster)
    {
        float result = 0;
        foreach (MonsterActionCondition AC in condition)
        {
            result += AC.Evaluate(_monster);
        }
        if (condition.Count > 0)
            result /= condition.Count;

        return result;
    }

    public void SetMonster(Monster _monster)
    {
        owner = _monster;
    }

    public void OnDoAction()
    {
        foreach (MonsterActionCondition MAC in condition)
            MAC.OnDoAction();
    }

    public virtual void EnterState() { }
    public virtual void UpdateState() { }
    public virtual void ExitState() { }
}
