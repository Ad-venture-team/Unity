using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterActionRoam : MonsterActionState
{
    public float speed;
    public float range;
    public float waitingTime;
    private float delay;
    private float threshold = .5f;

    private Vector3 targetPos;

    public override void EnterState(Monster _monster)
    {
        NewRoamPosition(_monster);
    }
    public override void UpdateState(Monster _monster)
    {
        if(delay > 0)
        {
            delay -= Time.deltaTime;
            return;
        }

        if (targetPos == null)
            NewRoamPosition(_monster);

        float dist = Vector2.Distance(_monster.transform.position, targetPos);
        if (dist <= threshold)
            NewRoamPosition(_monster);

        _monster.transform.position += (targetPos - _monster.transform.position).normalized * speed * Time.deltaTime;

    }

    public void NewRoamPosition(Monster _monster)
    {
        targetPos = _monster.transform.position + Random.insideUnitSphere * range;
        targetPos.z = 0;
        delay = waitingTime;
    }


}
