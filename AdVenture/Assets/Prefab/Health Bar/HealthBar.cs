using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Owner")]
    [SerializeField] private Monster owner;
    [Space]
    [Header("Bar")]
    [SerializeField] private RectTransform healthBar;

    private void Update()
    {
        healthBar.localScale = new Vector3((float)owner.health / (float)owner.maxHealth, 1,1);
    }
}
