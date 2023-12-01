using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class MonsterActionChase : MonsterActionState
{
    public float speed;
    public override void UpdateState(Monster _monster)
    {
        if (_monster.target == null)
            return;

        //Voir pour mettre un A* ici

        _monster.transform.position += (_monster.target.position - _monster.transform.position).normalized * speed * Time.deltaTime;
    }

    public override MonsterActionState GetCopy()
    {
        MonsterActionChase copy = new MonsterActionChase();
        copy.condition = new List<MonsterActionCondition>(condition);
        copy.speed = speed;
        return copy;
    }
}
