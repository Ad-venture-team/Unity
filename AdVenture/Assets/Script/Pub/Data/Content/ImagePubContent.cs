using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ImagePubContent : PubContent
{
    public Sprite icon;
    public override void LoadContent(PubUI _pub)
    {
        Image image;
        if (!_pub.TryGetComponent(out image))
            image = _pub.gameObject.AddComponent<Image>();
     
        image.sprite = icon;
    }
}
