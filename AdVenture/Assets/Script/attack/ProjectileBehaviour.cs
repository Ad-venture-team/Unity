using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBehaviour : MonoBehaviour {
    [HideInInspector] public WeaponData weaponData;
    [HideInInspector] public Vector2 target;
    protected float damageModificator;
    public abstract void SetData(Transform player, Transform monstre, WeaponData weaponData, float _dmgMod);
}
