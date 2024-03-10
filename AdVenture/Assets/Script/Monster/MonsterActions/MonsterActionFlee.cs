using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MonsterActionFlee : MonsterActionState
{
    private const float threshold = 0.2f;

    public float speed;
    private float fSpeed;
    private Vector3 waypoint;

    public override void EnterState(Monster _monster)
    {
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
            List<Vector2Int> path = Pathfinding.Instance.Flee(_monster.transform.position, _monster.target.position);
            if (path.Count == 0)
                return;
            Vector2Int goTo = path[0];
            waypoint = new Vector3(goTo.x, goTo.y, 0);
        }

        _monster.transform.position += (waypoint - _monster.transform.position).normalized * fSpeed * Time.deltaTime;
        _monster.visual.flipX = (waypoint - _monster.transform.position).normalized.x > 0;
    }

    public override MonsterActionState GetCopy()
    {
        MonsterActionFlee copy = new MonsterActionFlee();
        copy.condition = new List<MonsterActionCondition>(condition);
        copy.speed = speed;
        return copy;
    }
}
