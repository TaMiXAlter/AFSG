
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
       
        RenderTexture renderTexture = AIRenderer.texture as RenderTexture;
        if (renderTexture == null)
        {
            Debug.LogError("AIRenderer.texture 不是 RenderTexture，無法讀取像素！");
            return;
        }
        
        RenderTexture.active = renderTexture;

        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();
        
        RenderTexture.active = null;
        
        byte[] bytes = texture.EncodeToJPG(90);
        ResourceManager.Instance.SaveImage(bytes, texture);

        Debug.Log("圖片已成功儲存！");
        ResourceManager.Instance.SaveImage(bytes,texture);
    }
    
    public void NextScene()
    {
        webCamTexture.Stop();
        StateManagerRef.NextScene();
    }
}
