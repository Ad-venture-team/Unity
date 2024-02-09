using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHealth : PlayerUpgradeBehaviour
{
    public bool isMax;

    public override void DoUpgrade()
    {
        PlayerController.Instance.Heal((int)value, isMax);
    }
}
