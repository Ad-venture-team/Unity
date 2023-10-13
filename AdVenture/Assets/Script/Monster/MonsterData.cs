using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName ="M_",menuName ="Data/Monster")]
public class MonsterData : ScriptableObject, IData
{
    public int id;

    public string name;

    [TabGroup("Stat")] public float health;
    [TabGroup("Stat")] public float movementSpeed;
    [TabGroup("Stat")] public float damage;

    [TabGroup("Stat")] public float attackVision;
    [TabGroup("Stat")] public float attackSpeed;
    [TabGroup("Stat")] public float incantationTime;

    [TabGroup("Roam")] public float roamRange;
    [TabGroup("Roam")] public float roamSpeed;
    [TabGroup("Roam")] public float waitingTime;
    [TabGroup("Roam")] public float visionRange;

    [TabGroup("Visual")] public Sprite icon;
    [TabGroup("Visual")] public RuntimeAnimatorController animationController;

    [SerializeReference] public MonsterAttack attack;

    public int GetId()
    {
        return id;
    }
}
