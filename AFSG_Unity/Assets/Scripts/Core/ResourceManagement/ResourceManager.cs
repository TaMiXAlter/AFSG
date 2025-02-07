using System;
using System.Collections;
using System.Collections.Generic;
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
    
    public ResourceAssest GetResource()
    {
        return _resourceAssest;
    }
}
