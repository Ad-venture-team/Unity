using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MonsterActionFlee : MonsterActionState
{
    private const float threshold = 0.1f;

    public float speed;
    private Vector3 targetPosition;
    public override void EnterState(Monster _monster)
    {
        targetPosition = _monster.transform.position;
    }
    public override void UpdateState(Monster _monster)
    {
        if (_monster.target == null)
            return;

        if (Vector3.Distance(targetPosition,_monster.transform.position) <= threshold)
        {
            //Voir pour mettre un A* ici
            PathFinding pathTravel = new PathFinding(MapsManager.Instance.GetValidePlace(), _monster.gameObject.transform.position, _monster.target.position);
            List<Vector3> place = pathTravel.FindHighestPath();
            if (pathTravel.CheckCorner())
            {
                targetPosition = place[place.Count-1];
            }
            else targetPosition = pathTravel.FindHighestPath()[0];
        }

        _monster.transform.position += (targetPosition - _monster.transform.position).normalized * speed * Time.deltaTime;
    }

    public override MonsterActionState GetCopy()
    {
        MonsterActionFlee copy = new MonsterActionFlee();
        copy.condition = new List<MonsterActionCondition>(condition);
        copy.speed = speed;
        return copy;
    }
}
