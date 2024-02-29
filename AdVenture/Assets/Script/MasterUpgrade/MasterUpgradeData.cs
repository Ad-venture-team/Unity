using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMasterUpgrade", menuName = "Data/MasterUpgrade")]
public class MasterUpgradeData : ScriptableObject, IData
{
    public int id;
    public string name;
    [SerializeReference] public List<MasterUpgradeBehaviour> behaviours = new List<MasterUpgradeBehaviour>();

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
