
using UnityEngine;
using UnityEngine.UI;

public class AgreementState : StateBase
{
    private RawImage showImage;
    public override void Initialize()
    {
        showImage = transform.GetComponentInChildren<RawImage>();
        Texture2D AgreementImg = ResourceManager.Instance.GetResource().AgreementImg;
        showImage.texture = AgreementImg;
    }

    private void OnEnable()
    {
        Debug.Log("Agreement");
        MediaPipeManager.Instance.OnHandsRaise += OnHandsRaise;
    }
    
    private void OnHandsRaise()
    {
        MediaPipeManager.Instance.OnHandsRaise -= OnHandsRaise;
        StateManagerRef.NextScene();
    }
}
