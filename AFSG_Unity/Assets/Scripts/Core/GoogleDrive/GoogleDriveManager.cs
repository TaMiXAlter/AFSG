using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGoogleDrive;

public class GoogleDriveManager : MonoBehaviour
{
    [SerializeField] private Texture2D testImage;
    
    private void Start()
    {
        StartCoroutine(TryUpload(testImage.EncodeToPNG()));
    }
    IEnumerator TryUpload(byte[] data)
    {
        // string name = "test" + DateTime.Now.ToString() + ".png";
        var file = new UnityGoogleDrive.Data.File(){Name = "test", Content = data};
        var request = GoogleDriveFiles.Create(file);
        request.Fields = new List<string> { "id" };
        yield return request.Send();
        print(request.IsError);
        print(request.RequestData.Content);
        print(request.RequestData.Id);
    }
}
