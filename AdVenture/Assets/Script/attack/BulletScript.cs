using System.Linq;
using UnityEngine;


public class BulletScript : ProjectileBehaviour {

    [SerializeField] private float threshold = .005f;
    public override void SetData(Transform player, Transform monstre, WeaponData d) {
        target=monstre.position;
        weaponData = d;
    }

    void Update() {
        if (target==null)
            return;

        if (Vector2.Distance(transform.position, target) < threshold)
            Destroy(gameObject);

        transform.position += weaponData.speed * Time.deltaTime * ((Vector3)target - transform.position).normalized;
    }
}
