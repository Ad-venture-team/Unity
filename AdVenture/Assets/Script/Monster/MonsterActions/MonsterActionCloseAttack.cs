using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        delay = 0;
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
        _monster.visual.flipX = direction.x < 0;
        DrawPreview(_monster);
    }

    public override void ExitState(Monster _monster)
    {
        if(currentPreview != null)
            GameObject.Destroy(currentPreview.gameObject);
        currentPreview = null;
        UnCastAttack(_monster);
    }

    private void LaunchAttack(Monster _monster, float _angle, float _radius)
    {
        Vector2 targetDir = _monster.transform.position + Quaternion.Euler(0, 0, _angle) * direction;

        Debug.Log("Attack");

        _monster.transform.DOPunchPosition(direction, 0.1f);

        UnCastAttack(_monster);
    }

    private void DrawPreview(Monster _monster)
    {
        CastAttack(_monster);
        if (currentPreview == null)
            currentPreview = GameObject.Instantiate(previewPrefab, _monster.transform);

            currentPreview.DrawCirclePreview(_monster.transform.position, direction, angle, radius);
            currentPreview.SetValue(previewTime, () => LaunchAttack(_monster, angle, radius));
    }

    private void CastAttack(Monster _monster)
    {
        isCasting = true;
        _monster.lockInState = true;
        _monster.animator.speed = 2;
    }

    private void UnCastAttack(Monster _monster)
    {
        delay = attackSpeed;
        isCasting = false;
        _monster.lockInState = false;
        _monster.animator.speed = 1;
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
