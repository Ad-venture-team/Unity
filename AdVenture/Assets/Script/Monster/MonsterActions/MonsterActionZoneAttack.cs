using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterActionZoneAttack : MonsterActionState
{
    public float radius;

    public float attackSpeed;
    public float previewTime;

    private float delay;

    public MonsterCircleAttackPreview previewPrefab;
    private MonsterCircleAttackPreview currentPreview;
    private Vector2 direction;
    private bool isCasting = false;

    public override void EnterState(Monster _monster)
    {
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

        DrawPreview(_monster);
    }

    public override void ExitState(Monster _monster)
    {
        delay = attackSpeed;
        if (currentPreview != null)
            GameObject.Destroy(currentPreview.gameObject);
        currentPreview = null;
        isCasting = false;
    }

    private void LaunchAttack(Monster _monster, float _radius)
    {
        Debug.Log("Attack");

        isCasting = false;
    }

    private void DrawPreview(Monster _monster)
    {
        isCasting = true;
        if (currentPreview == null)
            currentPreview = GameObject.Instantiate(previewPrefab, _monster.transform);

        currentPreview.DrawCirclePreview(_monster.target.position, direction, 360, radius);
        currentPreview.SetValue(previewTime, () => LaunchAttack(_monster, radius));
    }

    public override MonsterActionState GetCopy()
    {
        MonsterActionZoneAttack copy = new MonsterActionZoneAttack();

        copy.condition = new List<MonsterActionCondition>(condition);
        copy.radius = radius;

        copy.attackSpeed = attackSpeed;
        copy.previewTime = previewTime;
        copy.previewPrefab = previewPrefab;

        return copy;
    }
}
