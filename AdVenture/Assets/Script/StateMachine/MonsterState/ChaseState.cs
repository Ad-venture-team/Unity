using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChaseState : AState<Monster>
{
    public new MonsterStateManager owner;
    public ChaseState(MonsterStateManager _owner) : base(_owner)
    {
        owner = _owner;
    }

    public override void CheckStateChange()
    {
        if (owner.item.IsInAttackRange())
            owner.SetAttackState();

        if (!owner.item.IsInVisionRange())
            owner.SetRoamState();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        owner.item.MoveToTarget();
    }
}
