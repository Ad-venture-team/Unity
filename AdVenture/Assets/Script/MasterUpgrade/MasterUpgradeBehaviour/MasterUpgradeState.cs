public class MasterUpgradeState : MasterUpgradeBehaviour
{
    public UpgradeType upgradeType;
    public override void DoUpgrade()
    {
        EventWatcher.DoAddMonsterUpgrade(value, upgradeType, type);
    }
}
