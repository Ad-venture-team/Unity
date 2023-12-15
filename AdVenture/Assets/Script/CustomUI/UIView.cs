using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class UIView : MonoBehaviour
{
    public RectTransform rect;
    public Canvas canvas;
    public RectTransform canvasRect;
    public CanvasGroup group;
    public UITransition slideType;

    public Vector2 originalPos;

    public bool notInteractable;

    public bool isFade;
    public float fadeTime;

    public bool isSliding;
    public float slideTime;

    public bool isShow;
    public bool isTweening;

    private void OnValidate()
    {
        rect = (RectTransform)transform;
        group = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
            canvasRect = canvas.gameObject.GetComponent<RectTransform>();

        if (fadeTime <= 0)
            fadeTime = UIConst.FADE_TIME;
        if (slideTime <= 0)
            slideTime = UIConst.SLIDE_TIME;
    }

    void Start()
    {
        rect = (RectTransform)transform;
        group = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        canvasRect = canvas.gameObject.GetComponent<RectTransform>();
        Vector2 localPos = rect.localPosition;
        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(0, 0);
        rect.localPosition = localPos;
        originalPos = rect.anchoredPosition;

        if (fadeTime <= 0)
            fadeTime = UIConst.FADE_TIME;
        if (slideTime <= 0)
            slideTime = UIConst.SLIDE_TIME;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Show(Action onShowEnd = null)
    {
        if (isShow)
            return;

        //if (isTweening)
        //    return;

        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        isTweening = true;
        isShow = true;

        if (isFade)
        {
            DOTween.To(() => group.alpha = 0, x => group.alpha = x, 1, fadeTime);
            if (!notInteractable)
            {
                group.blocksRaycasts = true;
                group.interactable = true;
            }
        }
        else
        {
            gameObject.SetActive(true);
        }

        if (slideType == UITransition.NONE)
            EndTweening(onShowEnd);
        else
        {
            switch (slideType)
            {
                case UITransition.TOP:
                    rect.DOAnchorPosY(Mathf.Abs(canvasRect.sizeDelta.y - originalPos.y), slideTime).OnComplete(() => { EndTweening(onShowEnd); });
                    break;
                case UITransition.RIGHT:
                    rect.DOAnchorPosX(Mathf.Abs(canvasRect.sizeDelta.x - originalPos.x), slideTime).OnComplete(() => { EndTweening(onShowEnd); });
                    break;
                case UITransition.BOTTOM:
                    rect.DOAnchorPosY(Mathf.Abs(canvasRect.sizeDelta.y + originalPos.y), slideTime).OnComplete(() => { EndTweening(onShowEnd); });
                    break;
                case UITransition.LEFT:
                    rect.DOAnchorPosX(Mathf.Abs(canvasRect.sizeDelta.x + originalPos.x), slideTime).OnComplete(() => { EndTweening(onShowEnd); });
                    break;
            }
        }
    }

    public void Hide(Action onHideEnd = null)
    {
        if (!isShow)
            return;
        //Debug.Log("Hide");
        //if (isTweening)
        //    return;

        isTweening = true;
        isShow = false;

        if (isFade)
        {
            DOTween.To(() => group.alpha = 1, x => group.alpha = x, 0, fadeTime);
            group.blocksRaycasts = false;
            group.interactable = false;
        }
        else
        {
            gameObject.SetActive(false);
        }

        if (slideType == UITransition.NONE)
            EndTweening(onHideEnd);
        else
        {
            rect.DOAnchorPos(originalPos, slideTime).OnComplete(() => { EndTweening(onHideEnd); });
            /*
            switch (slideType)
            {
                case UITransition.TOP:
                    rect.DOAnchorPosY(Mathf.Abs(canvasRect.sizeDelta.y + originalPos.y), slideTime).OnComplete(() => { EndTweening(); });
                    break;
                case UITransition.RIGHT:
                    rect.DOAnchorPosX(Mathf.Abs(canvasRect.sizeDelta.x + originalPos.x), slideTime).OnComplete(() => { EndTweening(); });
                    break;
                case UITransition.BOTTOM:
                    rect.DOAnchorPosY(-Mathf.Abs(canvasRect.sizeDelta.y + originalPos.y), slideTime).OnComplete(() => { EndTweening(); });
                    break;
                case UITransition.LEFT:
                    rect.DOAnchorPosX(-Mathf.Abs(canvasRect.sizeDelta.x + originalPos.x), slideTime).OnComplete(() => { EndTweening(); });
                    break;
            }
            */
        }
    }

    private void EndTweening(Action onEnd = null)
    {
        isTweening = false;
        onEnd?.Invoke();
    }

    #region FORCE_HIDE/SHOW

    public void ForceShow()
    {
        if (isShow)
            return;

        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        isShow = true;

        if (isFade)
        {

            group.alpha = 1;
            group.blocksRaycasts = true;
            group.interactable = true;
        }
        else
        {
            gameObject.SetActive(true);
        }

        if (slideType == UITransition.NONE)
            isTweening = false;

        switch (slideType)
        {
            case UITransition.TOP:
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, (Mathf.Abs(canvasRect.sizeDelta.y - rect.anchoredPosition.y)));
                break;
            case UITransition.RIGHT:
                rect.anchoredPosition = new Vector2(Mathf.Abs(canvasRect.sizeDelta.x - rect.anchoredPosition.x), rect.anchoredPosition.y);
                break;
            case UITransition.BOTTOM:
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, Mathf.Abs(canvasRect.sizeDelta.y + rect.anchoredPosition.y));
                break;
            case UITransition.LEFT:
                rect.anchoredPosition = new Vector2(Mathf.Abs(canvasRect.sizeDelta.x + rect.anchoredPosition.x), rect.anchoredPosition.y);
                break;
        }
    }

    public void ForceHide()
    {
        if (!isShow)
            return;

        isShow = false;

        if (isFade)
        {
            group.alpha = 0;
            group.blocksRaycasts = false;
            group.interactable = false;
        }
        else
        {
            gameObject.SetActive(false);
        }

        if (slideType == UITransition.NONE)
            isTweening = false;

        switch (slideType)
        {
            case UITransition.TOP:
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, (Mathf.Abs(canvasRect.sizeDelta.y + rect.anchoredPosition.y)));
                break;
            case UITransition.RIGHT:
                rect.anchoredPosition = new Vector2(Mathf.Abs(canvasRect.sizeDelta.x + rect.anchoredPosition.x), rect.anchoredPosition.y);
                break;
            case UITransition.BOTTOM:
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, -Mathf.Abs(canvasRect.sizeDelta.y + rect.anchoredPosition.y));
                break;
            case UITransition.LEFT:
                rect.anchoredPosition = new Vector2(-Mathf.Abs(canvasRect.sizeDelta.x + rect.anchoredPosition.x), rect.anchoredPosition.y);
                break;
        }
    }
    #endregion
}
