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

    [Header("Util")] public Texture2D NudeImg;
    
    public ResourceAssest _resourceAssest; 
    private string SaveImagePath = Application.dataPath + "/Resources/SaveImage";

    public string ImageDataPathTemp = "";
    public ResourceAssest GetResource()
    {
        return _resourceAssest;
    }

    public void SaveImage(byte[] data ,Texture2D textureSave)
    {
        DateTime currentDateTime = DateTime.Now;
        string formattedDateTime = currentDateTime.ToString("yyyyMMdd_HHmmss");
        
        ImageDataPathTemp =
            Path.Combine(SaveImagePath, formattedDateTime + ".jpg");
        File.WriteAllBytes(ImageDataPathTemp, data);
        
        Debug.Log($"已儲存到: {ImageDataPathTemp}");
        
        NudeImg = textureSave;
    }
}
