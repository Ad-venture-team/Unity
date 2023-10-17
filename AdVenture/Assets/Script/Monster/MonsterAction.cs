using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class MonsterAction
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

    public abstract void DoAction(Monster _monster);

    public void OnDoAction()
    {
        foreach (MonsterActionCondition MAC in condition)
            MAC.OnDoAction();
    }
}
