using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NudeImgGetter : MonoBehaviour
{
    private void OnEnable()
    {
        this.GetComponent<RawImage>().texture = ResourceManager.Instance.NudeImg;
    }
}
