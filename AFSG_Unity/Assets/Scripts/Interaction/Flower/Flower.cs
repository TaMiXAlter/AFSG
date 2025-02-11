
using System;
using UnityEngine;

public class Flower : MonoBehaviour
{
    private RectTransform _rectTransform;
    private float canvaWidth;
    private bool goRight = false;
    private bool goLeft = false;
    
    [SerializeField] private float moveSpeed = 100f;
    
    float moveDelta = 0.0f;
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }


    private void Update()
    {
        if (goRight) {
            if(_rectTransform.anchoredPosition.x >canvaWidth/2 ) Destroy(this.gameObject);
            moveDelta = moveSpeed;
        }

        if (goLeft)
        {
            if(_rectTransform.anchoredPosition.x < -canvaWidth/2 ) Destroy(this.gameObject);
            moveDelta = -moveSpeed;
        }
        
        if(moveDelta == 0) return;
        _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x + moveDelta * Time.deltaTime, _rectTransform.anchoredPosition.y);
    }

    public void Init(Vector2 pos,float inCanvaWidth)
    {
        _rectTransform.anchoredPosition = pos;
        canvaWidth = inCanvaWidth;
    }


    public void WaveRight()
    {
        if ( _rectTransform.anchoredPosition.x < -(canvaWidth / 4) )
        {
            goLeft = true;
            return;
        }
        goRight = true;
        return;
    }
    
    
    public void WaveLeft()
    {
        if ( _rectTransform.anchoredPosition.x > (canvaWidth / 4) )
        {
            goRight = true;
            return;
        }
        goLeft = true;
        return;
    }
}
