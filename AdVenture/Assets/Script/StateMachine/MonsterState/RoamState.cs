using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamState : AState<Monster>
{
    public new MonsterStateManager owner;
    public RoamState(MonsterStateManager _owner) : base(_owner) 
    {
        owner = _owner;
    }

    public override void CheckStateChange()
    {
        if (owner.item.IsInVisionRange())
            owner.SetChaseState();
    }

    public override void EnterState()
    {
        base.EnterState();
        owner.item.NewRoamPosition();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        owner.item.Roam();
    }
}
