using UnityEngine;
using System.Collections.Generic;

public class MasterUpgradeManager : SingletonBehaviour<MasterUpgradeManager>
{
    private Dictionary<MonsterType, Dictionary<UpgradeType, List<float>>> upgrades = new Dictionary<MonsterType, Dictionary<UpgradeType, List<float>>>();
    private void Awake()
    {
        EventWatcher.onAddMonsterUpgrade += AddMonsterUpgrade;
    }

    private void OnDestroy()
    {
        EventWatcher.onAddMonsterUpgrade -= AddMonsterUpgrade;
    }

    private void AddMonsterUpgrade(float _value, UpgradeType _type, MonsterType _monster)
    {
        Dictionary<UpgradeType, List<float>> dico;

        if (!upgrades.ContainsKey(_monster))
            upgrades.Add(_monster, new Dictionary<UpgradeType, List<float>>());

        dico = upgrades[_monster];
     
        if (dico.ContainsKey(_type))
            dico[_type].Add(_value);
        else
            dico.Add(_type, new List<float> { _value });
    }

    public List<float> GetUpgradesValue(MonsterType _monster, UpgradeType _type)
    {
        List<float> result = new List<float>();

        if(upgrades.ContainsKey(MonsterType.NONE))
            foreach(var val in upgrades[MonsterType.NONE])
                if (val.Key == _type)
                    result.AddRange(val.Value);

        if(_monster != MonsterType.NONE && upgrades.ContainsKey(_monster))
            foreach (var val in upgrades[_monster])
                if (val.Key == _type)
                    result.AddRange(val.Value);

        return result;
    }
}
