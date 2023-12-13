using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomEditorCreator : UIViewManager
{
    public RoomElementUI elementPrefab;
    public Transform container;

    private List<RoomElementUI> elems = new List<RoomElementUI>();

    private int width = 7;
    private int height = 10;


    public static event Action<RoomElementItem> onNewRoomElem;
    public static void DoOnNewRoomElem(RoomElementItem _item) => onNewRoomElem?.Invoke(_item);

    private void Awake()
    {
        onNewRoomElem += OnNewItem;
    }

    private void OnDestroy()
    {
        onNewRoomElem -= OnNewItem;
    }

    private void ClearContainer()
    {
        foreach (Transform T in container)
            T.gameObject.SetActive(false);

        elems.Clear();
    }

    public void OnNewItem(RoomElementItem _item)
    {
        RoomElementUI current;

        if (elems.Exists(x => x.item == _item))
        {
            current = elems.Find(x => x.item == _item);
            current.ChangeQuantity(1);
        }
        else
        {
            current = Instantiate(elementPrefab, container);
            current.SetData(_item);
            current.onClick = () => OnClickCallback(current);
            elems.Add(current);
        }
        Show();
    }

    private void OnClickCallback(RoomElementUI _elem)
    {
        bool needToRemove = _elem.ChangeQuantity(-1);
        if (needToRemove)
        {
            elems.Remove(_elem);
            Destroy(_elem.gameObject);
            if (elems.Count <= 0)
                Hide();
        }
    }

    public override void Hide()
    {
        base.Hide();
        ClearContainer();
    }

    public void CreateNewRoom()
    {
        Room newRoom = new Room(width, height);
        foreach(RoomElementUI elem in elems)
        {
            for (int i = 0; i < elem.quantity; i++)
            {
                RoomElement newElem = new RoomElement();
                newElem.id = elem.item.id;
                newElem.posX = RandomUtils.GetRandom(0, width);
                newElem.posY = RandomUtils.GetRandom(0, height);
                switch (elem.item.type)
                {
                    case RoomElementType.MONSTER:
                        newRoom.monsters.Add(newElem);
                        break;
                    default:
                        break;
                }
            }
        }
        EventWatcher.DoOnNewRoom(newRoom);
    }


}
