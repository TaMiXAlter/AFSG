using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "Resource", menuName = "ScriptableObjects/ResourceAssest", order = 1)]
public class ResourceAssest : ScriptableObject
{
    [Header("Idle State")] 
    public VideoClip IdleVideoClip;
    
    [Header("Opening State")]
    public VideoClip OpeningVideoClip;
    
    [Header("Agreement State")]
    public Texture2D AgreementImg;
    
    [Header("Start State")]
    public VideoClip StartVideoClip;
    
    [Header("Monitor State")]
    public VideoClip MonitorBGVideo;
    
    [Header("Outro State")]
    public VideoClip OutroVideo;
}
