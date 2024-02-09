using UnityEngine;
public class UpgradeStat : PlayerUpgradeBehaviour
{
    [Tooltip("AttackSpeed is in percent other in flat")]
    [SerializeField] UpgradeType type;
    public override void DoUpgrade()
    {
        PlayerController.Instance.AddUpgrade(type, value);
    }

}
