using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;


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
    
    public event Action OnHandsWaveRight;
    public event Action OnHandsWaveLeft;

    public event Action<float,float> OnBodyMove;
    
    public Vector3[] landMarks = new Vector3[]{};

    [Header("HandWave Para")] 
    private float rightHandWaveDelta = 0.0f;
    private float leftHandWaveDelta = 0.0f;
    
    private Queue<Vector3> rightHandTemp =new Queue<Vector3>();
    private Queue<Vector3> leftHandTemp =new Queue<Vector3>();
    
    [SerializeField] private int HandTempLength = 10;
    [SerializeField] private float HandDeltaTrigger = 3.0f;
    private void Awake()
    {
        if (MediaPipeManager.Instance != this) Destroy(this);
        client = new UdpClient(port);
    }
    private void Update()
    {
        String udpData = "";
        ReceiveData(ref udpData);
        
        if (udpData == "")
        {
            Debug.LogError("Nodata");
            return;
        }
        
        landMarks = SortData(udpData);
        
        //hand raise
        if (isHandsRaise(landMarks) && OnHandsRaise != null)
        {
            OnHandsRaise.Invoke();
            //make hand raise do once 
            OnHandsRaise = null;
        }
        
        //hand wave
        if (OnHandsWaveRight != null || OnHandsWaveLeft != null)
        {
            if (TryGetHandsWaveDelta(landMarks))
            {
                if (TryTriggerHandsWave())
                {
                    rightHandTemp.Clear();
                    leftHandTemp.Clear();
                    rightHandWaveDelta = 0.0f;
                    leftHandWaveDelta = 0.0f;
                }
            }
        }
        // follow body
        if (OnBodyMove != null) OnBodyMove.Invoke(landMarks[0].x , landMarks[0].y);
        
       

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

    private Vector3[] SortData( String input)
    {
        input = input.Remove(0, 1);
        input = input.Remove(input.Length - 1, 1);
        
        String[] dataSplit = input.Split(',');
        var output = new Vector3[33];
        for (int i = 0; i < 33; i++)
        {
            float x = float.Parse(dataSplit[i * 3]);
            float y = float.Parse(dataSplit[i * 3 + 1]);
            float z = float.Parse(dataSplit[i * 3 + 2]);
            Vector3 temp = new Vector3(x,y,z);
            output[i] = temp;
        }

        return output;
    }

    private bool TryGetHandsWaveDelta(Vector3[] input)
    {
        Vector3 nowRight,nowLeft;
        if (input[15] == Vector3.zero || input[16] == Vector3.zero) return false;
        
        nowRight = input[16];
        nowLeft = input[15];
        rightHandTemp.Enqueue(nowRight);
        leftHandTemp.Enqueue(nowLeft);
        
        if (rightHandTemp.Count < HandTempLength || leftHandTemp.Count < HandTempLength) return true;
        
        Vector3 privRight,privLeft;
        privRight = rightHandTemp.Dequeue();
        privLeft = leftHandTemp.Dequeue();
        rightHandWaveDelta = nowRight.x - privRight.x;
        leftHandWaveDelta = nowLeft.x - privLeft.x;
        
        return true;
    }
    // 判斷器
    bool isHandsRaise(Vector3[] input)
    {
        if (input[15].y>input[13].y && input[13].y > input[11].y &&
        input[16].y>input[14].y && input[14].y > input[12].y)
        {
            return true;
        }
        
        return false;
    }

    bool TryTriggerHandsWave()
    {
        if (rightHandWaveDelta > HandDeltaTrigger || leftHandWaveDelta > HandDeltaTrigger)
        {
            Debug.Log("Right: " + rightHandWaveDelta + " Left: " + leftHandWaveDelta);
            OnHandsWaveLeft?.Invoke();
            return true;
            
        }

        if (rightHandWaveDelta < -HandDeltaTrigger || leftHandWaveDelta < -HandDeltaTrigger)
        {
            Debug.Log("Right: " + rightHandWaveDelta + " Left: " + leftHandWaveDelta);
            OnHandsWaveRight?.Invoke();
            return true;
        }

        return false;
    }
}
