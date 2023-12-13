using System;
using UnityEngine;

public class boomerang : MonoBehaviour {
    Vector2 StartPoint;
    float BezierTime = 0;

    [SerializeField] float speed = 1f;
    [SerializeField] float rotationSpeed = 1f;
    Vector2[] Controls = new Vector2[6];

    [SerializeField] Transform player;
    bool isReturning = false;
    int Bezier = 1;

    void Start() {
        Vector2 closestMob=new Vector2(3, -2);
        Vector2 dirMob = (closestMob-(Vector2)transform.position).normalized*6;

        Controls[0]=(Vector2)transform.position+dirMob+PerpendicularCw(dirMob).normalized*2+(-dirMob).normalized*3;
        Controls[1]=(Vector2)transform.position+dirMob+PerpendicularCw(dirMob).normalized*2;
        Controls[2]=(Vector2)transform.position+dirMob+PerpendicularCw(dirMob).normalized*2+dirMob.normalized*3;
        Controls[3]=(Vector2)transform.position+dirMob+PerpendicularCCw(dirMob).normalized*2+dirMob.normalized*3;
        Controls[4]=(Vector2)transform.position+dirMob+PerpendicularCCw(dirMob).normalized*2;
        Controls[5]=(Vector2)transform.position+dirMob+PerpendicularCCw(dirMob).normalized*2+(-dirMob).normalized*3;

        Debug.DrawLine(transform.position, (Vector2)transform.position+dirMob, Color.red, 1);
        Debug.DrawLine((Vector2)transform.position+dirMob, Controls[1], Color.blue, 1);
        Debug.DrawLine((Vector2)transform.position+dirMob, Controls[4], Color.cyan, 1);
        Debug.DrawLine(Controls[1], Controls[0], Color.green, 1);
        Debug.DrawLine(Controls[1], Controls[2], Color.white, 1);
        Debug.DrawLine(Controls[4], Controls[3], Color.magenta, 1);
        Debug.DrawLine(Controls[4], Controls[5], Color.gray, 1);

        StartPoint=transform.position;

        float tMob = (closestMob-(Vector2)transform.position).magnitude/6;
        Vector2 pointOnBezier = new();
        if (tMob>(1.5-0.125)) {
            Debug.Log("out of range");
            Destroy(gameObject);
        } else if (tMob>1) {
            tMob--;
            pointOnBezier = Bezier3(Controls[1], Controls[2], Controls[3], Controls[4], tMob);
        } else {
            pointOnBezier = Bezier2(StartPoint, Controls[0], Controls[1], tMob);
        }
        Debug.DrawLine(pointOnBezier+new Vector2(0.1f,0.1f), pointOnBezier-new Vector2(0.1f, 0.1f), Color.magenta, 1);
        Debug.DrawLine(pointOnBezier+new Vector2(0.1f, -0.1f), pointOnBezier-new Vector2(0.1f, -0.1f), Color.magenta, 1);
        float angl = Mathf.Acos(((closestMob-(Vector2)transform.position).sqrMagnitude+(pointOnBezier-(Vector2)transform.position).sqrMagnitude-(pointOnBezier-closestMob).sqrMagnitude)/(2*(closestMob-(Vector2)transform.position).magnitude*(pointOnBezier-(Vector2)transform.position).magnitude));
        Debug.DrawLine(RotatePointAroundPivot(closestMob, transform.position, -angl)+new Vector2(0.1f, 0.1f), RotatePointAroundPivot(closestMob, transform.position, -angl)-new Vector2(0.1f, 0.1f), Color.black, 1);
        Debug.DrawLine(RotatePointAroundPivot(closestMob, transform.position, -angl)+new Vector2(0.1f, -0.1f), RotatePointAroundPivot(closestMob, transform.position, -angl)-new Vector2(0.1f, -0.1f), Color.black, 1);
        for(int i=0; i<Controls.Length; i++) {
            Controls[i]=RotatePointAroundPivot(Controls[i], transform.position, angl);
        }
        Debug.DrawLine(Controls[0], Controls[2], Color.black, 1);
        Debug.DrawLine(Controls[3], Controls[5], Color.black, 1);
    }

    void Update() {
        transform.Rotate(0, 0, rotationSpeed*Time.deltaTime*100);

        BezierTime=BezierTime+Time.deltaTime*speed;

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
}
