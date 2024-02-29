using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MonsterActionFlee : MonsterActionState
{
    public float speed;
    private float fSpeed;

    public override void EnterState(Monster _monster)
    {
        List<float> speedMod = MasterUpgradeManager.Instance.GetUpgradesValue(_monster.data.type, UpgradeType.SPEED);
        fSpeed = speed;
        for (int i = 0; i < speedMod.Count; i++)
            fSpeed += speedMod[i] * speed;
    }
    public override void UpdateState(Monster _monster)
    {
        if (_monster.target == null)
            return;

        //Voir pour mettre un A* ici
        PathFinding pathTravel = new PathFinding(MapsManager.Instance.GetValidePlace(), _monster.gameObject.transform.position, _monster.target.position);
        List<Vector3> place = pathTravel.FindHighestPath();
        Vector3 goTo = place[0];

        Debug.Log(_monster.data.name + " Flee");

        _monster.transform.position += (goTo - _monster.transform.position).normalized * fSpeed * Time.deltaTime;
        _monster.visual.flipX = (goTo - _monster.transform.position).normalized.x < 0;
    }

    public override MonsterActionState GetCopy()
    {
        MonsterActionFlee copy = new MonsterActionFlee();
        copy.condition = new List<MonsterActionCondition>(condition);
        copy.speed = speed;
        return copy;
    }
}
