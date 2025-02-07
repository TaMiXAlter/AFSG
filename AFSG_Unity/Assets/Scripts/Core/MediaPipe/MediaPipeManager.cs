using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;


struct LandMark
{
    public LandMark(float _x, float _y, float _z)
    {
        x = _x;
        y = _y;
        z = _z;
    }
    public float x;
    public float y;
    public float z;
}
public class MediaPipeManager : MonoBehaviour
{
    #region Singleton
    private static MediaPipeManager _instance;

    public static MediaPipeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MediaPipeManager>();
            }
            return _instance;
        }
    }
    #endregion
    
    private UdpClient client;
    private int port = 6500;
    public event Action OnHandsRaise;
    private void Awake()
    {
        if (MediaPipeManager.Instance != this) Destroy(this);
        client = new UdpClient(port);
        
    }


    private void Update()
    {
        String udpData = "";
        ReceiveData(ref udpData);
        
        if (OnHandsRaise == null) return;
        
        if (udpData == "")
        {
            Debug.LogError("Nodata");
            return;
        }
        
        LandMark[] landMarks = SortData(udpData);
        
        if (isHandsRaise(landMarks))
        {
            Debug.Log("Raise");
            OnHandsRaise.Invoke();
            //make hand raise do once 
            OnHandsRaise = null;
        }
    }

    private void ReceiveData(ref String output)
    {
        try
        {
            IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
            byte[] getdata = client.Receive(ref anyIP);
            output = Encoding.UTF8.GetString(getdata);
        }
        catch (Exception err)
        {
            print(err.ToString());
        }
    }

    private LandMark[] SortData( String input)
    {
        input = input.Remove(0, 1);
        input = input.Remove(input.Length - 1, 1);
        
        String[] dataSplit = input.Split(',');
        var output = new LandMark[33];
        for (int i = 0; i < 33; i++)
        {
            float x = float.Parse(dataSplit[i * 3]);
            float y = float.Parse(dataSplit[i * 3 + 1]);
            float z = float.Parse(dataSplit[i * 3 + 2]);
            LandMark temp = new LandMark(x,y,z);
            output[i] = temp;
        }

        return output;
    }
    
    
    // 判斷器
    bool isHandsRaise(LandMark[] input)
    {
        if (input[15].y>input[13].y && input[13].y > input[11].y &&
        input[16].y>input[14].y && input[14].y > input[12].y)
        {
            return true;
        }
        
        return false;
    }
}
