using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class bulletScript : MonoBehaviour {
    Vector2 target;
    [SerializeField, Range(5f, 20f)] float speed = 5f;
    [SerializeField] mouseMove mouseMove;

    void Start() {
        target=getClosestMob();
        if (target==null)
            Destroy(gameObject);
    }
    void Update() {
        transform.position+=speed*Time.deltaTime*new Vector3((target-new Vector2(transform.position.x, transform.position.y)).normalized.x, (target-new Vector2(transform.position.x, transform.position.y)).normalized.y, 0);
    }

    public Vector2 getClosestMob() {
        GameObject[] mobs = mouseMove.mobs;
        return mobs.OrderBy(t => (t.transform.position-transform.position).sqrMagnitude).FirstOrDefault().transform.position;
    }
}
