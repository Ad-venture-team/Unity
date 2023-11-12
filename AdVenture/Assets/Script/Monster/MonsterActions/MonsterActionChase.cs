using UnityEngine;

public class MonsterActionChase : MonsterActionState
{
    public float speed;
    public override void UpdateState(Monster _monster)
    {
        if (_monster.target == null)
            return;

        //Voir pour mettre un A* ici

        _monster.transform.position += (_monster.target.position - _monster.transform.position).normalized * speed * Time.deltaTime;
    }
}
