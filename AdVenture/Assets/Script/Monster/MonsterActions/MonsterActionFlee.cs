using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterActionFlee : MonsterActionState
{
    public float speed;
    public override void UpdateState()
    {
        if (owner.target == null)
            return;

        //Voir pour mettre un A* ici

        owner.transform.position += (owner.transform.position - owner.target.position).normalized * speed * Time.deltaTime;
    }
}
