using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoManager : MonoBehaviour
{
    #region Singleton
        private static VideoManager _instance;
        public static VideoManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.Find("VideoManager").GetComponent<VideoManager>();
                }

                return _instance;
            }
        }
    #endregion
    private VideoPlayer _videoPlayer;

    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _videoPlayer.isLooping = false;
    }
    
    public void PlayVideo(VideoClip videoClip,bool loop = false)
    {
        if(loop) _videoPlayer.isLooping = true;
        else _videoPlayer.isLooping = false;
        
        _videoPlayer.clip = videoClip;
        _videoPlayer.Play();
    }

    public void StopVideo()
    {
        _videoPlayer.clip = null;
        _videoPlayer.isLooping = false;
        _videoPlayer.Stop();
    }
    
    public void BindOnVideoEnd(VideoPlayer.EventHandler action)
    {
        _videoPlayer.loopPointReached += action;
    }
    
    public void UnBindOnVideoEnd(VideoPlayer.EventHandler action)
    {
        _videoPlayer.loopPointReached -= action;
    }
}
