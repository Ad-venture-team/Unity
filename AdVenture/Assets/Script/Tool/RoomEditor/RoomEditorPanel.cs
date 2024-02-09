using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class RoomEditorPanel : UIViewManager
{
    public CustomButtonList navBar;

    public RoomElementUI elementPrefab;
    public Transform container;

    private List<RoomElementUI> elems = new List<RoomElementUI>();

    private void Awake()
    {
        navBar.Init();
    }

    private void ClearContainer()
    {
        foreach (Transform T in container)
            T.gameObject.SetActive(false);
    }
    
    public void LoadMonsters()
    {
        ClearContainer();
        List<MonsterData> monsterData = DataBase.Instance.monsterData.Values.ToList();
        for (int i = 0; i < monsterData.Count; i++)
        {
            if (elems.Count <= i)
                elems.Add(Instantiate(elementPrefab, container));

            RoomElementUI current = elems[i];
            RoomElementItem newItem = new RoomElementItem(monsterData[i].id, RoomElementType.MONSTER);
            current.SetData(newItem);
            current.onClick = () => RoomEditorCreator.DoOnNewRoomElem(newItem);
            current.gameObject.SetActive(true);
        }
    }

    public void LoadPubs()
    {
        ClearContainer();
        List<PubData> pubData = DataBase.Instance.pubData.Values.ToList();
        for (int i = 0; i < pubData.Count; i++)
        {
            if (elems.Count <= i)
                elems.Add(Instantiate(elementPrefab, container));

            RoomElementUI current = elems[i];
            RoomElementItem newItem = new RoomElementItem(pubData[i].id, RoomElementType.PUB);
            current.SetData(newItem);
            current.onClick = () => EventWatcher.DoOnNewPub(newItem.id, Vector2.zero);
            current.gameObject.SetActive(true);
        }
    }

    public void LoadWeapon()
    {
        ClearContainer();
        List<WeaponData> weaponData = DataBase.Instance.weaponData.Values.ToList();
        for (int i = 0; i < weaponData.Count; i++)
        {
            if (elems.Count <= i)
                elems.Add(Instantiate(elementPrefab, container));

            RoomElementUI current = elems[i];
            RoomElementItem newItem = new RoomElementItem(weaponData[i].id, RoomElementType.WEAPON);
            current.SetData(newItem);
            current.onClick = () => PlayerController.Instance.SetWeapon(newItem.id);
            current.gameObject.SetActive(true);
        }
    }
    public void LoadPlayerUpgrade()
    {
        ClearContainer();
        List<PlayerUpgradeData>upgradeData = DataBase.Instance.upgradeData.Values.ToList();
        for (int i = 0; i < upgradeData.Count; i++)
        {
            if (elems.Count <= i)
                elems.Add(Instantiate(elementPrefab, container));

            RoomElementUI current = elems[i];
            RoomElementItem newItem = new RoomElementItem(upgradeData[i].id, RoomElementType.UPGRADE);
            current.SetData(newItem);
            current.onClick = () => DataBase.Instance.upgradeData[newItem.id].GetUpgrade();
            current.gameObject.SetActive(true);
        }
    }

    public void FixedUpdate()
    {
        if (Keyboard.current.ctrlKey.isPressed && Keyboard.current.shiftKey.isPressed && Keyboard.current.rKey.wasPressedThisFrame)
                Show();
    }
}
