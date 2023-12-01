using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventWatcher
{
    #region MONSTER

    public static event Action<Monster> onAddMonster;
    public static void DoOnAddMonster(Monster _monster) => onAddMonster?.Invoke(_monster);

    public static event Action<Monster> onRemoveMonster;
    public static void DoOnRemoveMonster(Monster _monster) => onRemoveMonster?.Invoke(_monster);

    public static event Action<Monster> onMonsterDie;
    public static void DoOnMonsterDie(Monster _monster) => onMonsterDie?.Invoke(_monster);

    public delegate void OnGetValue<T>(ref T _value);
    public static event OnGetValue<List<Monster>> getMonsterList;
    public static void DoGetMonsterList(ref List<Monster> _allMonster) => getMonsterList?.Invoke(ref _allMonster);

    #endregion
}
