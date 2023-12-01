using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class boomerang : MonoBehaviour {
    Vector2 StartPoint;
    Vector2 ControlPoint;
    Vector2 ControlPoint2;
    Vector2 EndPoint;
    float BezierTime = 0;

    [SerializeField] float speed = 1f;
    [SerializeField] float rotationSpeed = 1f;

    [SerializeField] Transform player;
    [SerializeField] Transform fullBezier;
    Vector2[] Controls = new Vector2[6];
    bool isReturning = false;
    int Bezier = 1;

    void Start() {
        int i=0;
        foreach (Transform child in fullBezier) {
            Controls[i]=child.transform.position;
            i++;
        }

        StartPoint=player.position;
        ControlPoint=Controls[0];
        EndPoint=Controls[1];
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
                ControlPoint=Controls[2];
                ControlPoint2=Controls[3];
                EndPoint=Controls[4];
                Bezier=2;
            } else if (Bezier==2) {
                ControlPoint=Controls[5];
                Bezier=3;
                isReturning=true;
            }
        }

        if (Bezier==1)
            transform.position=Bezier2(StartPoint, ControlPoint, EndPoint, BezierTime);
        else if (Bezier==2)
            transform.position=Bezier3(StartPoint, ControlPoint, ControlPoint2, EndPoint, BezierTime);
        else if (Bezier==3)
            transform.position=Bezier2(StartPoint, ControlPoint, player.position, BezierTime);
    }

    Vector2 Bezier2(Vector2 s, Vector2 c, Vector2 e, float t) {
        return (((1-t)*(1-t))*s)+(2*t*(1-t)*c)+((t*t)*e);
    }

    //Vector3 Bezier2(Vector3 s, Vector3 c, Vector3 e, float t) {
    //    return (((1-t)*(1-t))*s)+(2*t*(1-t)*c)+((t*t)*e);
    //}

    Vector2 Bezier3(Vector2 s, Vector2 c1, Vector2 c2, Vector2 e, float t) {
        return (((-s+3*(c1-c2)+e)*t+(3*(s+c2)-6*c1))*t+3*(c1-s))*t+s;
    }

    //Vector3 Bezier3(Vector3 s, Vector3 c1, Vector3 c2, Vector3 e, float t) {
    //    return (((-s+3*(c1-c2)+e)*t+(3*(s+c2)-6*c1))*t+3*(c1-s))*t+s;
    //}


    //Vector2 target;
    //[SerializeField, Range(0, 20f)] float speed = 5f;
    //public Vector2 getClosestMob() {
    //        GameObject[] mobs = GameObject.FindGameObjectsWithTag("mob");
    //        return mobs.OrderBy(t => (t.transform.position-transform.position).sqrMagnitude).FirstOrDefault().transform.position;
    //    }
}
