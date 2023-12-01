using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseMove : MonoBehaviour {
    [SerializeField] attacks attacks;
    [SerializeField, Range(0f, 10f)] float delay = 1f;
    bool isAttackOn = false;
    public GameObject[] mobs;


    private void Start() {
        mobs=GameObject.FindGameObjectsWithTag("mob");
        StartCoroutine("autoAttack");
    }

    void Update() {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 screenCorner = Camera.main.ScreenToWorldPoint(new Vector2(1,1))+GetComponent<SpriteRenderer>().bounds.size/2;
        transform.position=mousePosition.Clamp(screenCorner, -screenCorner);

        if (Input.GetMouseButtonDown(0)) {
            isAttackOn=!isAttackOn;
        }
    }
    private IEnumerator autoAttack() {
        while (true) {
            if (isAttackOn) {
                attacks.attack();
            }
            yield return new WaitForSeconds(delay);
        }
    }
}
