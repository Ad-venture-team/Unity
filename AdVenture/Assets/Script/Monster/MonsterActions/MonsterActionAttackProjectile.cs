using UnityEngine;

public class MonsterActionAttackProjectile : MonsterActionState
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

    public override void EnterState()
    {
        delay = attackSpeed;
    }
    public override void UpdateState()
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

            Vector2 targetDir = owner.transform.position + Quaternion.Euler(0, 0, currentAngle) * (owner.target.position - owner.transform.position).normalized;

            MonsterProjectile newProjectile = GameObject.Instantiate(projectilePrefab);
            newProjectile.InitVector(targetDir, owner.transform.position);
            newProjectile.Init(projectileSpeed);
            newProjectile.SetIcon(icon);
        }
        delay = attackSpeed;
    }
}
