using UnityEngine;

public class MonsterActionAttackProjectile : MonsterAction
{
    public MonsterProjectile projectilePrefab;
    public float projectileSpeed;
    public Sprite icon;


    public int nProjectile;
    public int angle;
    public bool randomAngle;

    public float attackSpeed;
    public float previewTime;

    private float delay;


    public override void DoAction(Monster _monster)
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
            return;
        }

        for (int i = 0; i < nProjectile; i++)
        {
            int currentAngle;
            if (randomAngle)
                currentAngle = Random.Range(-angle / 2, angle / 2);
            else
                currentAngle = ((angle / (nProjectile - 1)) * i) - (angle / 2);

            Vector2 targetDir = _monster.transform.position + Quaternion.Euler(0, 0, currentAngle) * (_monster.target.position - _monster.transform.position).normalized;

            MonsterProjectile newProjectile = GameObject.Instantiate(projectilePrefab);
            newProjectile.InitVector(targetDir, _monster.transform.position);
            newProjectile.Init(projectileSpeed);
            newProjectile.SetIcon(icon);
        }
        delay = attackSpeed;
    }
}
