using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CustomButtonList
{
    public List<CustomButton> buttons = new List<CustomButton>();
    public CustomButton currentButton;

    public void Init()
    {
        foreach (CustomButton CB in buttons)
            CB.onClick.AddListener(() => ForceCurrentButton(CB));
        if (currentButton == null)
            return;
        currentButton.forceSelect = true;
        currentButton.OnSelect(null);
        currentButton.onClick.Invoke();
        currentButton.OnPointerEnter(null);
    }

    public void AddButton(CustomButton btn)
    {
        buttons.Add(btn);
        btn.onClick.AddListener(() => ForceCurrentButton(btn));
    }
    
    private void ForceCurrentButton(CustomButton button)
    {
        if (currentButton != null)
        {
            currentButton.forceSelect = false;
            currentButton.OnPointerExit(null);
        }
        currentButton = button;
        currentButton.forceSelect = true;
    }

    private int GetCurrentButtonIndex()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (currentButton == buttons[i])
                return i;
        }
        return 0;
    }

    public void Next()
    {
        if (currentButton == null)
        {
            currentButton = buttons[0];
            currentButton.OnPointerClick(null);
            return;
        }
        int currentBtnIndex = GetCurrentButtonIndex();
        CustomButton nextButton = buttons[(currentBtnIndex + 1) % buttons.Count];
        nextButton.OnSelect(null);
        nextButton.onClick.Invoke();
        currentButton.OnPointerEnter(null);
    }

    public void Activate()
    {
        foreach (CustomButton CB in buttons)
            CB.gameObject.SetActive(true);
    }
    public void Desactivate()
    {
        foreach (CustomButton CB in buttons)
            CB.gameObject.SetActive(false);
    }

    public void Clear()
    {
        if (currentButton != null)
        {
            currentButton.isSelect = false;
            currentButton.OnDeselect(null);
        }
        currentButton = null;
        buttons.Clear();
    }

}
