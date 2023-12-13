using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
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
        _monster.animator.SetBool("isMoving", delay < 0);

        if(delay > 0)
        {
            delay -= Time.deltaTime;
            return;
        }

        if (targetPos == null)
            NewRoamPosition(_monster);

        _monster.transform.position += (targetPos - _monster.transform.position).normalized * speed * Time.deltaTime;
        _monster.visual.flipX = (targetPos - _monster.transform.position).normalized.x < 0;

        float dist = Vector2.Distance(_monster.transform.position, targetPos);
        if (dist <= threshold)
            NewRoamPosition(_monster);
    }

    public override void ExitState(Monster _monster)
    {
        _monster.animator.SetBool("isMoving", false);
    }

    public void NewRoamPosition(Monster _monster)
    {
        targetPos = _monster.transform.position + UnityEngine.Random.insideUnitSphere * range;
        targetPos.z = 0;
        delay = waitingTime;
    }

    public override MonsterActionState GetCopy()
    {
        MonsterActionRoam copy = new MonsterActionRoam();
        copy.condition = new List<MonsterActionCondition>(condition);
        copy.speed = speed;
        copy.range = range;
        copy.waitingTime = waitingTime;
        return copy;
    }
}
