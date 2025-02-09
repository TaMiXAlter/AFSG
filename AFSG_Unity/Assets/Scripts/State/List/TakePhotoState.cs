
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.WebCam;

public class TakePhotoState : StateBase
{
    private WebCamTexture webCamTexture;
    
    private RawImage VideoRenderer;
    private RawImage PhotoRenderer;
    private RawImage AIRenderer;
    
    private Texture2D PhotoTextureTemp;
    public override void Initialize()
    {
        webCamTexture = new WebCamTexture("OBS Virtual Camera");
        VideoRenderer = transform.Find("VideoRenderer").GetComponent<RawImage>();
        PhotoRenderer = transform.Find("PhotoRenderer").GetComponent<RawImage>();
        AIRenderer = transform.Find("AIRenderer").GetComponent<RawImage>();
        
    }

    private void OnEnable()
    {
        VideoRenderer.texture = webCamTexture;
        webCamTexture.Play();
   
    }

    public void TryShowPhoto()
    {
        PhotoTextureTemp = new Texture2D(webCamTexture.width, webCamTexture.height);
        PhotoTextureTemp.SetPixels(webCamTexture.GetPixels());
        PhotoTextureTemp.Apply();
        PhotoRenderer.texture = PhotoTextureTemp;
    }

    public void TrySaveAIPhoto()
    {
        Texture2D texture =
            new Texture2D(AIRenderer.texture.width, AIRenderer.texture.height, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, 0, AIRenderer.texture.width, AIRenderer.texture.height), 0, 0);

        byte[] bytes = texture.EncodeToJPG(90);
        string savePath =
            Path.Combine(Application.persistentDataPath, DateTime.Now.ToString() + ".jpg");
        File.WriteAllBytes(savePath, bytes);
        
        Debug.Log($"已儲存到: {savePath}");
    }
}
