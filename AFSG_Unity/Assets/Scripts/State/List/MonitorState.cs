using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MonitorState : StateBase
{
    public override void Initialize()
    {
        
    }

    private void OnEnable()
    {
        VideoClip IdleVideoClip = ResourceManager.Instance.GetResource().MonitorBGVideo;
        VideoManager.Instance.PlayVideo(IdleVideoClip,false);
        VideoManager.Instance.BindOnVideoEnd(OnVideoEnd);
    }

    private void OnVideoEnd(VideoPlayer source)
    {
        VideoManager.Instance.UnBindOnVideoEnd(OnVideoEnd);
        StateManagerRef.NextScene();
    }
}


