using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PlayerUpgradeUI : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI header;
    public TextMeshProUGUI desc;
    public Image icon;

    private PlayerUpgradeData data;

    public void OnPointerClick(PointerEventData eventData)
    {
        data.GetUpgrade();
    }

    public void SetData(PlayerUpgradeData _data)
    {
        data = _data;
        header.text = data.name;
        desc.text = data.desc;
    }
}
