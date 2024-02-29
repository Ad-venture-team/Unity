using System;

[Serializable]
public abstract class MasterUpgradeBehaviour
{
    public float value;
    public MonsterType type;
    public abstract void DoUpgrade();
}
