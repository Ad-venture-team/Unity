using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStateManager : AStateManager<Monster>
{
    public MonsterStateManager(Monster _item) : base(_item) 
    {
        SetRoamState();
    }

    public void SetRoamState()
    {
        ChangeState(new RoamState(this));
    }

    public void SetChaseState()
    {
        ChangeState(new ChaseState(this));
    }

    public void SetAttackState()
    {
        ChangeState(new AttackState(this));
    }
}
