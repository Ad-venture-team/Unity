using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerUpgradeCanvas : UIViewManager
{
    public PlayerUpgradeUI prefab;
    void Awake()
    {
        EventWatcher.onRequestPlayerUpgrade += LoadUI;
    }

    private void OnDestroy()
    {
        EventWatcher.onRequestPlayerUpgrade -= LoadUI;
    }

    // Update is called once per frame
    private void LoadUI()
    {
        List<PlayerUpgradeData> upgrades = DataBase.Instance.upgradeData.Values.ToList();
        List<PlayerUpgradeData> selectedUpgrades = new List<PlayerUpgradeData>();
        for (int i = 0; i < 3; i++)
        {

        }
    }
}
