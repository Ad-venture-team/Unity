using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempMobScript : MonoBehaviour {
    [SerializeField, Range(0f, 10f)] float spinSpeed = 4f;
    [SerializeField] int PV = 10;

    void Update() {
        transform.Rotate(0, 0, 10*spinSpeed*Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag=="bullet") {
            Destroy(collision.gameObject);
            PV--;
            if (PV == 0) Destroy(gameObject);
        }
    }
}
