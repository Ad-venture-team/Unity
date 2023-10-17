using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName ="M_",menuName ="Data/Monster")]
public class MonsterData : ScriptableObject, IData
{
    public int id;

    public string name;

    public float health;

    public Sprite icon;
    public RuntimeAnimatorController animationController;

    [SerializeReference] public List<MonsterAction> actions = new List<MonsterAction>();

    public int GetId()
    {
        return id;
    }
}
