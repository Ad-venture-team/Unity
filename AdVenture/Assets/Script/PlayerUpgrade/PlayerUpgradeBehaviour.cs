using System;

[Serializable]
public abstract class PlayerUpgradeBehaviour
{
    public float value;

    public abstract void DoUpgrade();
}
