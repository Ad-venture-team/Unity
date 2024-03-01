using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventWatcher
{
    public static event Action<Room> onNewRoom;
    public static void DoOnNewRoom(Room _room) => onNewRoom?.Invoke(_room);    
    
    public static event Action onEndRoom;
    public static void DoOnEndRoom() => onEndRoom?.Invoke();

    public static event Action<int, Vector2> onNewPub;
    public static void DoOnNewPub(int _id, Vector2 _position) => onNewPub?.Invoke(_id,_position);

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

    public static event Action<float, UpgradeType, MonsterType> onAddMonsterUpgrade;
    public static void DoAddMonsterUpgrade(float _value,UpgradeType _upgrade, MonsterType _type) => onAddMonsterUpgrade?.Invoke(_value, _upgrade, _type);

    #endregion

    #region REQUEST

    public static event Action onRequestPlayerUpgrade;
    public static void DoRequestPlayerUpgrade() => onRequestPlayerUpgrade?.Invoke();

    #endregion
}
