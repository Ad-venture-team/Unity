using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName ="M_",menuName ="Data/Monster")]
public class MonsterData : ScriptableObject, IData
{
    public int id;

    public string name;

    public MonsterType type;

    public int maxHealth;
    public int baseAttack;

    public List<ItemResource> cost;

    public Sprite icon;
    public RuntimeAnimatorController animationController;

    [SerializeReference] public List<MonsterActionState> actions = new List<MonsterActionState>();

    public int GetId()
    {
        return id;
    }

    public List<MonsterActionState> GetActions()
    {
        List<MonsterActionState> states = new List<MonsterActionState>();
        foreach (MonsterActionState MAS in actions)
            states.Add(MAS.GetCopy());
        return states;
    }
}
