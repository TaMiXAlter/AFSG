using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NudeImgTest : MonoBehaviour
{
    private RawImage AIRenderer;

    private void Start()
    {
        
        AIRenderer = transform.Find("AIRenderer").GetComponent<RawImage>();

        StartCoroutine(SaveNudeImageCoroutine());
    }

    IEnumerator SaveNudeImageCoroutine()
    {
        yield return new WaitForEndOfFrame();
        SaveNudeImage();
    }
    private void SaveNudeImage()
    {
        
        if (AIRenderer.texture == null)
        {
            Debug.LogError("AIRenderer.texture 為 null，請確認是否已經賦值！");
            return;
        }

        RenderTexture renderTexture = AIRenderer.texture as RenderTexture;
        if (renderTexture == null)
        {
            Debug.LogError("AIRenderer.texture 不是 RenderTexture，無法讀取像素！");
            return;
        }

        // 設置 RenderTexture 為當前讀取目標
        RenderTexture.active = renderTexture;

        // 創建與 RenderTexture 相同尺寸的 Texture2D
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
    
        // 讀取像素
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        // 清除 RenderTexture.active
        RenderTexture.active = null;

        // 儲存圖片
        byte[] bytes = texture.EncodeToJPG(90);
        ResourceManager.Instance.SaveImage(bytes, texture);

        Debug.Log("圖片已成功儲存！");
        ResourceManager.Instance.SaveImage(bytes,texture);
    }
}
