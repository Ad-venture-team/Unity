using UnityEngine;
[CreateAssetMenu(fileName = "Weapon", menuName = "Data/Weapon")]
public class WeaponData : ScriptableObject,IData {
    public int id;
    public int damage;
    public float range;
    public float speed;
    public float attackSpeed;
    public ProjectileBehaviour projectile;

    public void SetData(Transform player, Transform monstre) {
        ProjectileBehaviour p = Instantiate(projectile);
        p.SetData(player, monstre, this);
    }

    int IData.GetId() {
        return id;
    }
}
