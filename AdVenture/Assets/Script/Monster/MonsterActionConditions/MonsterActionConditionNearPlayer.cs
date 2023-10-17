using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterActionConditionNearPlayer : MonsterActionCondition
{
    public Vector2 distance;
    public override float Evaluate(Monster _monster)
    {
        float result = 0;

        float dist = Vector2.Distance(_monster.transform.position, _monster.target.position);

        if (dist < distance.x) return 0;

        if (dist > distance.y) return 0;

        result = 1 - dist / (distance.x + distance.y);

        return result;
    }
}
