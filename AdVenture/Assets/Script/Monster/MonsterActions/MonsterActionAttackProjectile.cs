using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class MonsterActionAttackProjectile : MonsterActionState
{
    public MonsterProjectile projectilePrefab;
    public float projectileSpeed;
    public Sprite icon;


    public int nProjectile;
    public float range;
    public float angle;
    public bool randomAngle;

    public float attackSpeed;
    public float previewTime;

    private float delay;

    public MonsterLineAttackPreview previewPrefab;
    private List<MonsterLineAttackPreview> previews = new List<MonsterLineAttackPreview>();
    private Vector2 direction;
    private List<float> angles = new List<float>();
    private bool isCasting = false;

    public override void EnterState(Monster _monster)
    {
        previews.Clear();
        isCasting = false;
    }
    public override void UpdateState(Monster _monster)
    {
        if (isCasting)
            return;

        if (delay > 0)
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
            float currentAngle;
            if (randomAngle)
                currentAngle = UnityEngine.Random.Range(-angle / 2, angle / 2);
            else
                currentAngle = ((angle / (nProjectile - 1)) * i) - (angle / 2);
            angles.Add(currentAngle);
        }
    }

    private void LaunchAttack(Monster _monster, float _angle)
    {
        Vector2 targetDir = _monster.transform.position + Quaternion.Euler(0, 0, _angle) * direction;

        MonsterProjectile newProjectile = GameObject.Instantiate(projectilePrefab);
        newProjectile.InitVector(targetDir, _monster.transform.position);
        newProjectile.Init(projectileSpeed, range);
        newProjectile.SetIcon(icon);
        delay = attackSpeed;
        isCasting = false;
    }

    private void DrawPreview(Monster _monster)
    {
        isCasting = true;
        for (int i = 0; i < angles.Count; i++)
        {
            float currentAngle = angles[i];
            MonsterLineAttackPreview currentPreview;
            if (i >= previews.Count)
                previews.Add(GameObject.Instantiate(previewPrefab, _monster.transform));

            currentPreview = previews[i];
            Vector2 targetDir = Quaternion.Euler(0, 0, currentAngle) * direction;
            currentPreview.DrawLinePreview(_monster.transform.position, targetDir, range, 1);
            currentPreview.SetValue(previewTime, () => LaunchAttack(_monster, currentAngle));
        }
    }

    public override MonsterActionState GetCopy()
    {
        MonsterActionAttackProjectile copy = new MonsterActionAttackProjectile();

        copy.condition = new List<MonsterActionCondition>(condition);
        copy.projectilePrefab = projectilePrefab;
        copy.projectileSpeed = projectileSpeed;
        copy.icon = icon;


        copy.nProjectile = nProjectile;
        copy.range = range;
        copy.angle = angle;
        copy.randomAngle= randomAngle;

        copy.attackSpeed= attackSpeed;
        copy.previewTime= previewTime;
        copy.previewPrefab = previewPrefab;

        return copy;
}
}
