using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackProjectile : MonsterAttack
{
    public MonsterProjectile projectilePrefab;

    public Sprite icon;

    public int nProjectile;
    public int angle;
    public bool randomAngle;

    public float projectileSpeed;

    private List<Vector2> allDirections = new List<Vector2>();

    public override void DoAttack(Monster _launcher, Transform _target)
    {
        for (int i = 0; i < nProjectile; i++)
        {
            int currentAngle;
            if (randomAngle)
                currentAngle = Random.Range(-angle / 2, angle / 2);
            else
                currentAngle = ((angle / (nProjectile-1)) * i) - (angle/2);

            Vector2 targetDir = _launcher.transform.position + Quaternion.Euler(0, 0, currentAngle) * (_target.position - _launcher.transform.position).normalized;

            MonsterProjectile newProjectile = GameObject.Instantiate(projectilePrefab);
            newProjectile.InitVector(targetDir, _launcher.transform.position);
            newProjectile.Init(projectileSpeed);
            newProjectile.SetIcon(icon);
        }
    }
    public override void DrawPreview(Monster _launcher, Transform _target)
    {
        for (int i = 0; i < nProjectile; i++)
        {
            int currentAngle;
            if (randomAngle)
                currentAngle = Random.Range(-angle / 2, angle / 2);
            else
                currentAngle = ((angle / (nProjectile - 1)) * i) - (angle / 2);

            Vector2 targetDir = _launcher.transform.position + Quaternion.Euler(0, 0, currentAngle) * (_target.position - _launcher.transform.position).normalized;

            allDirections.Add(targetDir);

        }
    }

    public override void DrawGizmo(Monster _launcher)
    {
        base.DrawGizmo(_launcher);
        if (_launcher.target == null)
            return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(_launcher.transform.position, _launcher.transform.position +  Quaternion.Euler(0, 0, angle/2) * (_launcher.target.position - _launcher.transform.position));
        Gizmos.DrawLine(_launcher.transform.position, _launcher.transform.position + Quaternion.Euler(0, 0, -angle/2) * (_launcher.target.position - _launcher.transform.position));
    }

}
