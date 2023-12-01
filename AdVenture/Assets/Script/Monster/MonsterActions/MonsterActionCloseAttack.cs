using System.Collections.Generic;
using UnityEngine;

public class MonsterActionCloseAttack : MonsterActionState
{
    public float angle;
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
        if(currentPreview != null)
            GameObject.Destroy(currentPreview.gameObject);
        currentPreview = null;
        isCasting = false;
    }

    private void LaunchAttack(Monster _monster, float _angle, float _radius)
    {
        Vector2 targetDir = _monster.transform.position + Quaternion.Euler(0, 0, _angle) * direction;

        Debug.Log("Attack");

        isCasting = false;
    }

    private void DrawPreview(Monster _monster)
    {
        isCasting = true;
        if (currentPreview == null)
            currentPreview = GameObject.Instantiate(previewPrefab, _monster.transform);

            currentPreview.DrawCirclePreview(_monster.transform.position, direction, angle, radius);
            currentPreview.SetValue(previewTime, () => LaunchAttack(_monster, angle, radius));
    }

    public override MonsterActionState GetCopy()
    {
        MonsterActionCloseAttack copy = new MonsterActionCloseAttack();

        copy.condition = new List<MonsterActionCondition>(condition);
        copy.angle = angle;
        copy.radius = radius;

        copy.attackSpeed = attackSpeed;
        copy.previewTime = previewTime;
        copy.previewPrefab = previewPrefab;

        return copy;
    }
}
