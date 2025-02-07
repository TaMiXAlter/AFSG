
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
    }
}
