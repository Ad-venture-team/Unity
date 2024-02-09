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
    [HideInInspector] public int quantity;

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
            case RoomElementType.PUB:
                PubData pubData = DataBase.Instance.pubData[_item.id];
                icon.sprite = null;
                header.text = pubData.name;
                break;
            case RoomElementType.WEAPON:
                WeaponData weaponData = DataBase.Instance.weaponData[_item.id];
                icon.sprite = null;
                header.text = weaponData.name;
                break;
            case RoomElementType.UPGRADE:
                PlayerUpgradeData upgradeData = DataBase.Instance.upgradeData[_item.id];
                icon.sprite = null;
                header.text = upgradeData.name;
                break;
            default:
                break;
        }
        quantityText.text = "";
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
