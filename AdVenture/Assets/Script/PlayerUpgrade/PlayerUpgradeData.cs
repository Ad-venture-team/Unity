using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName ="newPlayerUpgrade",menuName ="Data/PlayerUpgrade")]
public class PlayerUpgradeData : ScriptableObject, IData
{
    public int id;
    public string name;
    [SerializeReference] public List<PlayerUpgradeBehaviour> behaviours = new List<PlayerUpgradeBehaviour>();

    public void GetUpgrade()
    {
        for (int i = 0; i < behaviours.Count; i++)
            behaviours[i].DoUpgrade();
    }

    public int GetId()
    {
        return id;
    }
}