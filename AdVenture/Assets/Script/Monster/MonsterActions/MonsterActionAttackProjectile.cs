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

    public MonsterCircleAttackPreview previewPrefab;
    private MonsterCircleAttackPreview currentPreview;
    private Vector2 direction;
    private bool isCasting = false;

    public override void EnterState()
    {
        isCasting = false;
    }
    public override void UpdateState()
    {
        if (delay > 0 || isCasting)
        {
            delay -= Time.deltaTime;
            return;
        }
        direction = (owner.target.position - owner.transform.position).normalized;
        DrawPreview();
    }

    public override void ExitState()
    {
        delay = attackSpeed;
    }

    private void LaunchAttack()
    {
        for (int i = 0; i < nProjectile; i++)
        {
            int currentAngle;
            if (randomAngle)
                currentAngle = Random.Range(-angle / 2, angle / 2);
            else
                currentAngle = ((angle / (nProjectile - 1)) * i) - (angle / 2);

            Vector2 targetDir = owner.transform.position + Quaternion.Euler(0, 0, currentAngle) * direction;

            MonsterProjectile newProjectile = GameObject.Instantiate(projectilePrefab);
            newProjectile.InitVector(targetDir, owner.transform.position);
            newProjectile.Init(projectileSpeed);
            newProjectile.SetIcon(icon);
        }
        delay = attackSpeed;
        isCasting = false;
    }

    private void DrawPreview()
    {
        if (currentPreview == null)
            currentPreview = GameObject.Instantiate(previewPrefab, owner.transform);

        isCasting = true;
        currentPreview.DrawCirclePreview(owner.transform, owner.target.position, angle, 10);
        currentPreview.SetValue(previewTime, LaunchAttack);
    }
}
