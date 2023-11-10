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

    public override void EnterState()
    {
        NewRoamPosition(owner);
    }
    public override void UpdateState()
    {
        if(delay > 0)
        {
            delay -= Time.deltaTime;
            return;
        }

        if (targetPos == null)
            NewRoamPosition(owner);

        float dist = Vector2.Distance(owner.transform.position, targetPos);
        if (dist <= threshold)
            NewRoamPosition(owner);

        owner.transform.position += (targetPos - owner.transform.position).normalized * speed * Time.deltaTime;

    }

    public void NewRoamPosition(Monster _monster)
    {
        targetPos = _monster.transform.position + Random.insideUnitSphere * range;
        targetPos.z = 0;
        delay = waitingTime;
    }


}
