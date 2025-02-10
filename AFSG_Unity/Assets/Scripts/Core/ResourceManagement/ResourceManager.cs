using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    #region Singleton

    private static ResourceManager _instance;
    public static ResourceManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
            }

            return _instance;
        }
    }

    #endregion
   
    public ResourceAssest _resourceAssest; 
    private string SaveImagePath = Application.dataPath + "/Resources/SaveImage";

    public string ImageDataPathTemp = "";
    public ResourceAssest GetResource()
    {
        return _resourceAssest;
    }

    public void SaveImage(byte[] data)
    {
        ImageDataPathTemp =
            Path.Combine(SaveImagePath, DateTime.Now.ToString() + ".jpg");
        File.WriteAllBytes(ImageDataPathTemp, data);
        
        Debug.Log($"已儲存到: {ImageDataPathTemp}");
    }
}
