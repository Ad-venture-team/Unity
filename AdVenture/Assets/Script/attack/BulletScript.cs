using System.Linq;
using UnityEngine;


public class BulletScript : ProjectileBehaviour {
    public override void SetData(Transform player, Transform monstre, WeaponData d) {
        target=monstre.position;
        weaponData = d;
    }

    void Update() {
        if (target==null)
            return;

        transform.position+=weaponData.speed*Time.deltaTime*new Vector3((target-new Vector2(transform.position.x, transform.position.y)).normalized.x, (target-new Vector2(transform.position.x, transform.position.y)).normalized.y, 0);
    }

    //return mobs.OrderBy(t => (t.transform.position-transform.position).sqrMagnitude).FirstOrDefault().transform.position;
}
