using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPubContent : PubContent
{
    public VideoClip clip;
    public override void LoadContent(PubUI _pub)
    {
        VideoPlayer anim;
        if (!_pub.TryGetComponent(out anim))
            anim = _pub.gameObject.AddComponent<VideoPlayer>();

        RenderTexture texture = new RenderTexture((int)clip.width, (int)clip.height, 16, RenderTextureFormat.ARGB32);
        texture.Create();

        RawImage image;
        if (!_pub.TryGetComponent(out image))
            image = _pub.gameObject.AddComponent<RawImage>();

        anim.targetTexture = texture;
        image.texture = texture;

        image.rectTransform.sizeDelta = new Vector2(clip.width, clip.height);

        anim.clip = clip;
        anim.Play();
    }
}
