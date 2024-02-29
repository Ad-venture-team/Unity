using System.Linq;
using UnityEngine;
using System.Collections.Generic;


public class BulletScript : ProjectileBehaviour {

    [SerializeField] private float threshold = .005f;
    public override void SetData(Transform player, Transform monstre, WeaponData d, List<float> _dmgMod) {
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
        if (collision.gameObject.CompareTag("Player"))
            return;

        Monster monster;
        if (collision.TryGetComponent(out monster))
        {
            float fDmg = weaponData.damage;
            for (int i = 0; i < damageModificator.Count; i++)
            {
                fDmg += weaponData.damage * damageModificator[i];
            }
            monster.TakeDamage((int)fDmg);
        }

        Destroy(gameObject);
    }
}
