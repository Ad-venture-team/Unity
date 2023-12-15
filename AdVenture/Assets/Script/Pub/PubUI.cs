using System;
using UnityEngine;
using UnityEngine.UI;

public class PubUI : UIViewManager
{
    public RectTransform rectTransform;
    public CustomButton closeBtn;
    public Image fillDelay;

    private PubData data;
    private float delay;

    public void FixedUpdate()
    {
        if (data != null && data.hasDelay)
        {
            delay -= Time.deltaTime;
            fillDelay.fillAmount = delay / data.delay;
            if(delay <= 0)
            {
                fillDelay.gameObject.SetActive(false);
                if (data.closeOnEnd)
                    Hide();
                else
                    closeBtn.gameObject.SetActive(true);
            }
        }
    }
    public void SetData(PubData _data)
    {
        data = _data;
        rectTransform.sizeDelta = data.size;
        data.content.LoadContent(this);
        fillDelay.gameObject.SetActive(data.hasDelay && data.showDelay);
        if (data.hasDelay)
        {
            closeBtn.gameObject.SetActive(false);
            delay = data.delay;
        }
        Show();
    }

    public override void Hide(Action onHideEnd = null)
    {
        base.Hide(OnHide);
    }

    public void OnHide()
    {
        //RemoveDuPubManager
        Destroy(gameObject);
    }
}
