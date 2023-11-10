using UnityEngine;

public class MonsterActionChase : MonsterActionState
{
    public float speed;
    public override void UpdateState()
    {
        if (owner.target == null)
            return;

        //Voir pour mettre un A* ici

        owner.transform.position += (owner.target.position - owner.transform.position).normalized * speed * Time.deltaTime;
    }
}
