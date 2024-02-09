using System.Collections.Generic;
using UnityEngine;

public class DataBase : SingletonBehaviour<DataBase>
{
    public Dictionary<int, MonsterData> monsterData = new Dictionary<int, MonsterData>();
    public Dictionary<int, PubData> pubData = new Dictionary<int, PubData>();
    public Dictionary<int, TileData> tileData = new Dictionary<int, TileData>();
    public Dictionary<int, WeaponData> weaponData = new Dictionary<int, WeaponData>();

    void Awake()
    {
        LoadAll();
    }

    private void LoadAll()
    {
        LoadMonsters();
        LoadPubs();
        LoadTile();
        LoadWeapon();
    }

    private Dictionary<int, T> Load<T>(string path) where T : Object, IData
    {
        var dict = Resources.LoadAll<T>(path);

        var result = new Dictionary<int, T>(dict.Length);

        foreach (var pair in dict)
            result.Add(pair.GetId(), pair);

        return result;
    }

    private void LoadMonsters()
    {
        monsterData = Load<MonsterData>("Data/Monster");
    }
    private void LoadPubs()
    {
        pubData = Load<PubData>("Data/Pub");
    }
    private void LoadTile()
    {
        tileData = Load<TileData>("Data/Tile");
    }
    private void LoadWeapon()
    {
        weaponData = Load<WeaponData>("Data/Weapon");
    }


}