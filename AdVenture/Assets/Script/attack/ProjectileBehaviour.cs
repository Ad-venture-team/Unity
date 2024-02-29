using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBehaviour : MonoBehaviour {
    [HideInInspector] public WeaponData weaponData;
    [HideInInspector] public Vector2 target;
    protected List<float> damageModificator = new List<float>();
    public abstract void SetData(Transform player, Transform monstre, WeaponData weaponData, List<float> _dmgMods);
}
