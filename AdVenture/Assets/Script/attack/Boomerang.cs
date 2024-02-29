using UnityEngine;
using System.Collections.Generic;

public class Boomerang : ProjectileBehaviour {
    Vector2 StartPoint;
    float BezierTime = 0;

    float rotationSpeed = 5f;
    Vector2[] Controls = new Vector2[6];

    Transform player;
    bool isReturning = false;
    int Bezier = 1;

    public override void SetData (Transform _player, Transform monstre, WeaponData d, List<float> _dmgMod) {
        player=_player;
        weaponData=d;
        damageModificator = _dmgMod;
        target =monstre.position;
        Vector2 dirMob = (target-(Vector2)_player.position).normalized*6;

        Controls[0]=(Vector2)_player.position+dirMob+PerpendicularCw(dirMob).normalized*2+(-dirMob).normalized*3;
        Controls[1]=(Vector2)_player.position+dirMob+PerpendicularCw(dirMob).normalized*2;
        Controls[2]=(Vector2)_player.position+dirMob+PerpendicularCw(dirMob).normalized*2+dirMob.normalized*3;
        Controls[3]=(Vector2)_player.position+dirMob+PerpendicularCCw(dirMob).normalized*2+dirMob.normalized*3;
        Controls[4]=(Vector2)_player.position+dirMob+PerpendicularCCw(dirMob).normalized*2;
        Controls[5]=(Vector2)_player.position+dirMob+PerpendicularCCw(dirMob).normalized*2+(-dirMob).normalized*3;

        StartPoint=_player.position;

        float tMob = (target-(Vector2)_player.position).magnitude/6;
        Vector2 pointOnBezier = new();
        if (tMob>(1.5-0.125)) {
            Destroy(gameObject);
        } else if (tMob>1) {
            tMob--;
            pointOnBezier = Bezier3(Controls[1], Controls[2], Controls[3], Controls[4], tMob);
        } else {
            pointOnBezier = Bezier2(StartPoint, Controls[0], Controls[1], tMob);
        }

        float angl = Mathf.Acos(((target-(Vector2)_player.position).sqrMagnitude+(pointOnBezier-(Vector2)_player.position).sqrMagnitude-(pointOnBezier-target).sqrMagnitude)/(2*(target-(Vector2)_player.position).magnitude*(pointOnBezier-(Vector2)_player.position).magnitude));

        for(int i=0; i<Controls.Length; i++) {
            Controls[i]=RotatePointAroundPivot(Controls[i], _player.position, angl);
        }
    }

    void Update() {
        if (player==null)
            return;

        transform.Rotate(0, 0, rotationSpeed*Time.deltaTime*100);
        BezierTime=BezierTime+Time.deltaTime*weaponData.speed;

        if (BezierTime>=1) {
            if (isReturning)
                Destroy(gameObject);
            BezierTime=0;
            StartPoint=transform.position;
            if (Bezier==1) {
                Bezier=2;
            } else if (Bezier==2) {
                Bezier=3;
                isReturning=true;
            }
        }

        if (Bezier==1)
            transform.position=Bezier2(StartPoint, Controls[0], Controls[1], BezierTime);
        else if (Bezier==2)
            transform.position=Bezier3(StartPoint, Controls[2], Controls[3], Controls[4], BezierTime);
        else if (Bezier==3)
            transform.position=Bezier2(StartPoint, Controls[5], player.position, BezierTime);
    }

    Vector2 Bezier2(Vector2 s, Vector2 c, Vector2 e, float t) {
        return (((1-t)*(1-t))*s)+(2*t*(1-t)*c)+((t*t)*e);
    }

    Vector2 Bezier3(Vector2 s, Vector2 c1, Vector2 c2, Vector2 e, float t) {
        return (((-s+3*(c1-c2)+e)*t+(3*(s+c2)-6*c1))*t+3*(c1-s))*t+s;
    }

    Vector2 PerpendicularCCw(Vector2 v) {
        return new Vector2(-v.y, v.x);
    }
    Vector2 PerpendicularCw(Vector2 v) {
        return new Vector2(v.y, -v.x);
    }

    //Vector3 Bezier2(Vector3 s, Vector3 c, Vector3 e, float t) {
    //    return (((1-t)*(1-t))*s)+(2*t*(1-t)*c)+((t*t)*e);
    //}

    //Vector3 Bezier3(Vector3 s, Vector3 c1, Vector3 c2, Vector3 e, float t) {
    //    return (((-s+3*(c1-c2)+e)*t+(3*(s+c2)-6*c1))*t+3*(c1-s))*t+s;
    //}

    Vector2 RotatePointAroundPivot(Vector2 point, Vector2 pivot, float angleRad) {
        return new Vector2(
            Mathf.Cos(angleRad)*(point.x-pivot.x)-Mathf.Sin(angleRad)*(point.y-pivot.y)+pivot.x,
            Mathf.Sin(angleRad)*(point.x-pivot.x)+Mathf.Cos(angleRad)*(point.y-pivot.y)+pivot.y
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            return;

        Monster monster;
        if (collision.TryGetComponent(out monster))
        {
            float fDmg = weaponData.damage;
            for (int i = 0; i < damageModificator.Count; i++)
                fDmg += weaponData.damage * damageModificator[i];
            monster.TakeDamage((int)fDmg);
        }
    }
}
