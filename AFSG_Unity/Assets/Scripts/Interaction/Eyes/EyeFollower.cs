using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyeFollower : MonoBehaviour
{
    
    public Vector2 Max = new Vector2(15, 10);
    public Vector2 Min = new Vector2(-15, -10);

    private RectTransform eye;
    private void Awake()
    {
        eye = transform.Find("EyeBall").GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        MediaPipeManager.Instance.OnBodyMove += InstanceOnOnBodyMove;
    }

    private void InstanceOnOnBodyMove(float x, float y)
    {
        Vector2 pos = new Vector2(Min.x + (Max.x - Min.x)*(1-x), Min.y + (Max.y - Min.y)*y); ;
        eye.anchoredPosition = pos;
    }
}
