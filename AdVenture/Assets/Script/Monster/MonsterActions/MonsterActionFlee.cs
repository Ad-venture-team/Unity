using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MonsterActionFlee : MonsterActionState
{
    public float speed;
    public override void UpdateState(Monster _monster)
    {
        if (_monster.target == null)
            return;

        //Voir pour mettre un A* ici
        PathFinding pathTravel = new PathFinding(MapsManager.Instance.GetValidePlace(), _monster.gameObject.transform.position, _monster.target.position);
        List<Vector3> place = pathTravel.FindHighestPath();
        Vector3 goTo = place[1];

        Debug.Log(_monster.data.name + " Flee");

        _monster.transform.position += (goTo - _monster.transform.position).normalized * speed * Time.deltaTime;
    }

    public override MonsterActionState GetCopy()
    {
        MonsterActionFlee copy = new MonsterActionFlee();
        copy.condition = new List<MonsterActionCondition>(condition);
        copy.speed = speed;
        return copy;
    }
}
