
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.WebCam;

public class TakePhotoState : StateBase
{
    private WebCamDevice webCamDevice;
    private WebCamTexture webCamTexture;
    private RenderTexture AICapture;

    private RawImage VideoRenderer;
    private RawImage ImgRenderer;

    private Texture2D TextureTemp;
    public override void Initialize()
    {
        webCamDevice = WebCamTexture.devices[0];
        webCamTexture = new WebCamTexture(webCamDevice.name);
        
        AICapture = ResourceManager.Instance._resourceAssest.AICapture;
        
        VideoRenderer = transform.Find("VideoRenderer").GetComponent<RawImage>();
        ImgRenderer = transform.Find("ImgRenderer").GetComponent<RawImage>();
    }

    private void OnEnable()
    {
        VideoRenderer.texture = webCamTexture;
        webCamTexture.Play();
        
    }
}
