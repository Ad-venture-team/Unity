using System.Linq;
using UnityEngine;


public class BulletScript : ProjectileBehaviour {

    [SerializeField] private float threshold = .005f;
    public override void SetData(Transform player, Transform monstre, WeaponData d, float _dmgMod) {
        target=monstre.position;
        weaponData = d;
        damageModificator = _dmgMod;
    }

    void Update() {
        if (target==null)
            return;

        if (Vector2.Distance(transform.position, target) < threshold)
            Destroy(gameObject);

        transform.position += weaponData.speed * Time.deltaTime * ((Vector3)target - transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monster monster;
        if (collision.TryGetComponent(out monster))
            monster.TakeDamage(weaponData.damage + (int)damageModificator);

        Destroy(gameObject);
    }
}
