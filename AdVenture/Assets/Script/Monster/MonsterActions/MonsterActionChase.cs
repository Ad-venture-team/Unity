using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class MonsterActionChase : MonsterActionState
{
    private const float threshold = .2f;
    public float speed;
    private float fSpeed;
    private Vector3 waypoint;

    public override void EnterState(Monster _monster)
    {
        base.EnterState(_monster);
        _monster.animator.SetBool("isMoving", true);
        List<float> speedMod = MasterUpgradeManager.Instance.GetUpgradesValue(_monster.data.type, UpgradeType.SPEED);
        fSpeed = speed;
        for (int i = 0; i < speedMod.Count; i++)
            fSpeed += speedMod[i] * speed;
        waypoint = _monster.transform.position;
    }

    public override void UpdateState(Monster _monster)
    {
        if (_monster.target == null)
            return;

        if (Vector3.Distance(waypoint, _monster.transform.position) <= threshold)
        {
            Vector2Int goTo = Pathfinding.Instance.GetPath(_monster.transform.position, _monster.target.position)[0];
            waypoint = new Vector3(goTo.x, goTo.y, 0);
        }

        _monster.transform.position += (waypoint - _monster.transform.position).normalized * fSpeed * Time.deltaTime;
        _monster.visual.flipX = (waypoint - _monster.transform.position).normalized.x < 0;
    }

    public override void ExitState(Monster _monster)
    {
        base.EnterState(_monster);
        _monster.animator.SetBool("isMoving", false);
    }

    public override MonsterActionState GetCopy()
    {
        MonsterActionChase copy = new MonsterActionChase();
        copy.condition = new List<MonsterActionCondition>(condition);
        copy.speed = speed;
        return copy;
    }
}
