using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEditorCreator : UIViewManager
{
    public RoomElementUI elementPrefab;
    public Transform container;

    private List<RoomElementUI> elems = new List<RoomElementUI>();


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

    }


}
