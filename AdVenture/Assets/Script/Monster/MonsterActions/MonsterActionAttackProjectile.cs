using UnityEngine;
using System.Collections.Generic;

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

    public MonsterLineAttackPreview previewPrefab;
    private List<MonsterLineAttackPreview> previews = new List<MonsterLineAttackPreview>();
    private Vector2 direction;
    private List<int> angles = new List<int>();
    private bool isCasting = false;

    public override void EnterState(Monster _monster)
    {
        previews.Clear();
        isCasting = false;
    }
    public override void UpdateState(Monster _monster)
    {
        if (delay > 0 || isCasting)
        {
            delay -= Time.deltaTime;
            return;
        }
        direction = (_monster.target.position - _monster.transform.position).normalized;

        CalculateAngles();

        DrawPreview(_monster);
    }

    public override void ExitState(Monster _monster)
    {
        delay = attackSpeed;
        foreach (MonsterLineAttackPreview preview in previews)
            GameObject.Destroy(preview.gameObject);
        previews.Clear();
        isCasting = false;
    }

    private void CalculateAngles()
    {
        angles.Clear();
        for (int i = 0; i < nProjectile; i++)
        {
            int currentAngle;
            if (randomAngle)
                currentAngle = Random.Range(-angle / 2, angle / 2);
            else
                currentAngle = ((angle / (nProjectile - 1)) * i) - (angle / 2);
            angles.Add(currentAngle);
        }
    }

    private void LaunchAttack(Monster _monster,int _angle)
    {
        Vector2 targetDir = _monster.transform.position + Quaternion.Euler(0, 0, _angle) * direction;

        MonsterProjectile newProjectile = GameObject.Instantiate(projectilePrefab);
        newProjectile.InitVector(targetDir, _monster.transform.position);
        newProjectile.Init(projectileSpeed);
        newProjectile.SetIcon(icon);
        delay = attackSpeed;
        isCasting = false;
    }

    private void DrawPreview(Monster _monster)
    {
        isCasting = true;
        for (int i = 0; i < angles.Count; i++)
        {
            int currentAngle = angles[i];
            MonsterLineAttackPreview currentPreview;
            if (i >= previews.Count)
                previews.Add(GameObject.Instantiate(previewPrefab, _monster.transform));

            currentPreview = previews[i];
            Vector2 targetDir = Quaternion.Euler(0, 0, currentAngle) * direction;
            currentPreview.DrawLinePreview(_monster.transform, _monster.transform.position, targetDir, 10, 1);
            currentPreview.SetValue(previewTime, () => LaunchAttack(_monster, currentAngle));
        }
    }
}
