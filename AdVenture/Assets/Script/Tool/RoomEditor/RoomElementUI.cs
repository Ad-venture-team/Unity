using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class RoomElementUI : MonoBehaviour, IPointerClickHandler
{
    public Image icon;
    public TextMeshProUGUI header;
    public TextMeshProUGUI quantityText;
    private int quantity;

    public Action onClick;
    
    [HideInInspector] public RoomElementItem item;

    public void SetData(RoomElementItem _item)
    {
        item = _item;
        switch (_item.type)
        {
            case RoomElementType.MONSTER:
                MonsterData monsterData = DataBase.Instance.monsterData[_item.id];
                icon.sprite = monsterData.icon;
                header.text = monsterData.name;
                break;
            default:
                break;
        }
        ChangeQuantity(1);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        onClick?.Invoke();
    }

    public bool ChangeQuantity(int _value)
    {
        quantity += _value;

        if (quantity > 1)
            quantityText.text = "" + quantity;
        else
            quantityText.text = "";

        return quantity <= 0;
    }
}
