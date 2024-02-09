using UnityEngine;
using System.Collections.Generic;

public class PlayerUpgradeData : ScriptableObject, IData
{
    public int id;
    public string name;
    [SerializeReference] public List<PlayerUpgradeBehaviour> behaviours;

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
