using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class MonsterActionState
{
    [SerializeReference] public List<MonsterActionCondition> condition =  new List<MonsterActionCondition>();

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

    public void OnDoAction()
    {
        foreach (MonsterActionCondition MAC in condition)
            MAC.OnDoAction();
    }

    public virtual void EnterState(Monster _monster) { }
    public virtual void UpdateState(Monster _monster) { }
    public virtual void ExitState(Monster _monster) { }

    public abstract MonsterActionState GetCopy();
}
