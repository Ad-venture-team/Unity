using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MonsterActionZoneAttack : MonsterActionState
{
    public int damage;
    public float radius;

    public float attackSpeed;
    public float previewTime;

    private float delay;

    public MonsterCircleAttackPreview previewPrefab;
    private MonsterCircleAttackPreview currentPreview;
    private Vector2 direction;
    private Vector2 origine;
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
        origine = _monster.target.position;
        _monster.visual.flipX = direction.x < 0;
        DrawPreview(_monster);
    }

    public override void ExitState(Monster _monster)
    {
        if (currentPreview != null)
            GameObject.Destroy(currentPreview.gameObject);
        currentPreview = null;

        UnCastAttack(_monster);
    }

    private void LaunchAttack(Monster _monster, float _radius)
    {
        if (Vector2.Distance(origine, PlayerController.Instance.transform.position) <= radius)
            PlayerController.Instance.TakeDamage(damage);

        _monster.transform.DOPunchPosition(direction, 0.1f);

        UnCastAttack(_monster);
    }

    private void DrawPreview(Monster _monster)
    {
        CastAttack(_monster);
        if (currentPreview == null)
            currentPreview = GameObject.Instantiate(previewPrefab, _monster.transform);

        currentPreview.DrawCirclePreview(_monster.target.position, direction, 360, radius);
        currentPreview.SetValue(previewTime, () => LaunchAttack(_monster, radius));
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
        MonsterActionZoneAttack copy = new MonsterActionZoneAttack();

        copy.condition = new List<MonsterActionCondition>(condition);
        copy.damage = damage;
        copy.radius = radius;

        copy.attackSpeed = attackSpeed;
        copy.previewTime = previewTime;
        copy.previewPrefab = previewPrefab;

        return copy;
    }
}
