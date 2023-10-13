using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class AttackState : AState<Monster>
    {
        public new MonsterStateManager owner;
        public AttackState(MonsterStateManager _owner) : base(_owner)
        {
            owner = _owner;
        }

        public override void CheckStateChange()
        {
            if (!owner.item.IsInAttackRange())
                owner.SetChaseState();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            owner.item.DoAttack();
        }

    }
