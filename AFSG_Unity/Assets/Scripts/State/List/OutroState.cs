using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class OutroState : StateBase
{
    public override void Initialize()
    {
       
    }

    private void OnEnable()
    {
        VideoClip StartVideoClip = ResourceManager.Instance.GetResource().OutroVideo;
        VideoManager.Instance.PlayVideo(StartVideoClip);
        VideoManager.Instance.BindOnVideoEnd(OnVideoEnd);
    }

    private void OnVideoEnd(VideoPlayer source)
    {
        VideoManager.Instance.UnBindOnVideoEnd(OnVideoEnd);
        StateManagerRef.NextScene();
    }
}
