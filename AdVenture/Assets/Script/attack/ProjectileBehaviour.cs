using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBehaviour : MonoBehaviour {
    public WeaponData weaponData;
    public Vector2 target;
    public abstract void SetData(Transform player, Transform monstre, WeaponData weaponData);
}
