using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CustomButton : Button
{
    //ButtonState
    public bool selectable;
    //Text
    public TextMeshProUGUI text;
    public ColorBlock textColor = new ColorBlock();
    //Border
    public Image border;
    public ColorBlock borderColor = new ColorBlock();
    //Tweening
    public bool slideOnHover;
    public bool scaleOnHover;
    public UITransition transitionOnHover;

    public bool isHover;
    public bool isSelect;

    public bool forceSelect;
    public bool resetOnClick;

    public RectTransform rect;
    public Vector2 originalPosition = new Vector2();
    public Vector3 originalScale = new Vector3();

    new void Start()
    {
        base.Start();
        rect = (RectTransform)transform;
        originalScale = rect.localScale;
    }

    public void SetOriginalValues()
    {
        rect = (RectTransform)transform;
        originalPosition = rect.anchoredPosition;
    }

    #region BUTTON_INTERFACE
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsInteractable())
            return;
        base.OnPointerEnter(eventData);

        if (image != null)
            image.color = colors.highlightedColor;
        if (text != null)
            text.color = textColor.highlightedColor;
        if (border != null)
            border.color = textColor.highlightedColor;

        if (scaleOnHover)
        {
            rect.DOScale(originalScale * UIConst.SCALE_ON_HOVER, UIConst.SCALE_TIME);
        }

        if (slideOnHover)
        {
            switch (transitionOnHover)
            {
                case UITransition.TOP:
                    rect.DOAnchorPosY(originalPosition.y + UIConst.SLIDE_ON_HOVER,UIConst.SLIDE_TIME);
                    break;
                case UITransition.RIGHT:
                    rect.DOAnchorPosX(originalPosition.x + UIConst.SLIDE_ON_HOVER, UIConst.SLIDE_TIME);
                    break;
                case UITransition.BOTTOM:
                    rect.DOAnchorPosY(originalPosition.y - UIConst.SLIDE_ON_HOVER, UIConst.SLIDE_TIME);
                    break;
                case UITransition.LEFT:
                    rect.DOAnchorPosX(originalPosition.x - UIConst.SLIDE_ON_HOVER, UIConst.SLIDE_TIME);
                    break;
            }
        }

        isHover = true;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (!IsInteractable() || isSelect || forceSelect)
            return;
        base.OnPointerExit(eventData);

        if (image != null)
            image.color = colors.normalColor;
        if (text != null)
            text.color = textColor.normalColor;
        if (border != null)
            border.color = textColor.normalColor;

        if (scaleOnHover)
        {
            rect.DOScale(originalScale, UIConst.SCALE_TIME);
        }

        if (slideOnHover)
        {
            rect.DOAnchorPos(originalPosition, UIConst.SLIDE_TIME);
        }

        isHover = false;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (!IsInteractable())
            return;
        base.OnPointerUp(eventData);
        if (!isHover)
        {
            OnPointerExit(eventData);
            return;
        }
        OnPointerEnter(eventData);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!IsInteractable())
            return;
        base.OnPointerDown(eventData);
        if (image != null)
            image.color = colors.pressedColor;
        if (text != null)
            text.color = textColor.pressedColor;
        if (border != null)
            border.color = textColor.pressedColor;

    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!IsInteractable())
            return;
        if (selectable)
            base.OnPointerClick(eventData);
        else
        {
            if(resetOnClick)
                OnPointerExit(null);
            onClick.Invoke();
        }
    }

    public override void OnSelect(BaseEventData eventData)
    {
        if (!selectable)
            return;

        base.OnSelect(eventData);
        isSelect = true;
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        isSelect = false;
        OnPointerExit(null);
    }

    #endregion

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        if (image != null)
            image.color = colors.normalColor;
        if (text != null)
            text.color = textColor.normalColor;
        if (border != null)
            border.color = borderColor.normalColor;

        if (isSelect || forceSelect)
        {
            if (image != null)
                image.color = colors.selectedColor;
            if (text != null)
                text.color = textColor.selectedColor;
            if (border != null)
                border.color = borderColor.selectedColor;
        }

        rect = (RectTransform)transform;
    }
#endif
}
