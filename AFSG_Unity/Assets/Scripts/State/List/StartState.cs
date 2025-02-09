
using System;
using UnityEngine;
using UnityEngine.Video;

public class StartState : StateBase
{
    public override void Initialize()
    {
        
    }

    private void OnEnable()
    {
        Debug.Log("Start");
        VideoClip StartVideoClip = ResourceManager.Instance.GetResource().StartVideoClip;
        VideoManager.Instance.PlayVideo(StartVideoClip);
        VideoManager.Instance.BindOnVideoEnd(OnVideoEnd);
    }

    private void OnVideoEnd(VideoPlayer source)
    {
        VideoManager.Instance.UnBindOnVideoEnd(OnVideoEnd);
        StateManagerRef.NextScene();
    }
}
