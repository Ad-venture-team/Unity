using UnityEngine;
[CreateAssetMenu(fileName = "Weapon", menuName = "Data/Weapon")]
public class WeaponData : ScriptableObject,IData {
    public int id;
    public string name;
    public int damage;
    public float range;
    public float speed;
    public float attackDelay;
    public ProjectileBehaviour projectile;

    public void SetData(Transform player, Transform monstre,float _dmgMod) {
        ProjectileBehaviour p = Instantiate(projectile,player.position,Quaternion.identity,null);
        p.SetData(player, monstre, this,_dmgMod);
    }

    int IData.GetId() {
        return id;
    }
}
