using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class OpeningState : StateBase
{
    public override void Initialize()
    {
        Debug.Log("OPening");
    }

    private void OnEnable()
    {
        Debug.Log("Open");
        VideoClip OpeningVideoClip = ResourceManager.Instance.GetResource().OpeningVideoClip;
        VideoManager.Instance.PlayVideo(OpeningVideoClip);
        VideoManager.Instance.BindOnVideoEnd(OnVideoEnd);
    }

    private void OnVideoEnd(VideoPlayer source)
    {
        VideoManager.Instance.UnBindOnVideoEnd(OnVideoEnd);
        StateManagerRef.NextScene();
    }
}
