using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackMelee : MonsterAttack
{
    public float range;
    public float angle;
    public override void DoAttack(Monster _launcher, Transform _target)
    {
        throw new System.NotImplementedException();
    }

    public override void DrawPreview(Monster _launcher, Transform _target)
    {
        throw new System.NotImplementedException();
    }
}
