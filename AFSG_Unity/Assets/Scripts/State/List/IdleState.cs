using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IdleState :StateBase
{
    public override void Initialize()
    {
        
    }

    private void OnEnable()
    {
        MediaPipeManager.Instance.OnHandsRaise += OnHandsRaise;
        
        VideoClip IdleVideoClip = ResourceManager.Instance.GetResource().IdleVideoClip;
        VideoManager.Instance.PlayVideo(IdleVideoClip,true);
    }

    private void OnDisable()
    {
        VideoManager.Instance.StopVideo();
    }

    private void OnHandsRaise()
    {
        MediaPipeManager.Instance.OnHandsRaise -= OnHandsRaise;
        StateManagerRef.NextScene();
    }
}
