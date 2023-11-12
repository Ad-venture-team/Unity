using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterActionFlee : MonsterActionState
{
    public float speed;
    public override void UpdateState(Monster _monster)
    {
        if (_monster.target == null)
            return;

        //Voir pour mettre un A* ici

        _monster.transform.position += (_monster.transform.position - _monster.target.position).normalized * speed * Time.deltaTime;
    }
}
