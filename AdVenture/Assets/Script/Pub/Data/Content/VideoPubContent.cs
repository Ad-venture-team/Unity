using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoPubContent : PubContent
{
    public AnimationClip clip;
    public override void LoadContent(PubUI _pub)
    {
        Animation anim;
        if (!_pub.TryGetComponent(out anim))
            anim = _pub.gameObject.AddComponent<Animation>();

        anim.AddClip(clip,"pubClip");
        anim.clip = clip;
        anim.Play();
    }
}
