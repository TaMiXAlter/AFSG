using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandWaveTest : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        MediaPipeManager.Instance.OnHandsWaveLeft += InstanceOnOnHandsWaveLeft;
        MediaPipeManager.Instance.OnHandsWaveRight += InstanceOnOnHandsWaveRight;
    }

    private void OnDisable()
    {
        MediaPipeManager.Instance.OnHandsWaveLeft -= InstanceOnOnHandsWaveLeft;
        MediaPipeManager.Instance.OnHandsWaveRight -= InstanceOnOnHandsWaveRight;
    }

    private void InstanceOnOnHandsWaveRight()
    {
        Debug.Log("Right");
    }

    private void InstanceOnOnHandsWaveLeft()
    {
        Debug.Log("Left");
    }
}
