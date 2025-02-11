using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlowerInteraction : MonoBehaviour
{
    [Header("Flower")]
    public GameObject Flower;
    
    [Header("Canvas")]
    private RectTransform CanvaRectTransform ;
    private float CanvaWidth;
    private float CanvaHeight;
    
    [Header("Para")] 
    public bool isSpawningFlower = false;
    [SerializeField] private float flowerSpawnInterval = 0.3f;
    public void ToogleSpawningFlower(bool goSpawn)
    {
        if (goSpawn)
        {
            isSpawningFlower = true;
            StartCoroutine(SpawnFlower());
        }
        
        if (!goSpawn)
        {
            isSpawningFlower = false;
        }
    }

    public void DestroyAllFlower()
    {
        foreach (var flower in GetComponentsInChildren<Flower>())
        {
            Destroy(flower.gameObject);
        }
    }
    
    private void Awake()
    {
        CanvaRectTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();
        CanvaWidth = CanvaRectTransform.sizeDelta.x;
        CanvaHeight = CanvaRectTransform.sizeDelta.y;
    }

    private void Start()
    {
        ToogleSpawningFlower(true);
        
    }

    private void OnDestroy()
    {
        ToogleSpawningFlower(false);
    }

    private void OnEnable()
    {
        MediaPipeManager.Instance.OnHandsWaveRight += InstanceOnOnHandsWaveRight;
        MediaPipeManager.Instance.OnHandsWaveLeft += InstanceOnOnHandsWaveLeft;
    }

    private void OnDisable()
    {
        MediaPipeManager.Instance.OnHandsWaveRight -= InstanceOnOnHandsWaveRight;
        MediaPipeManager.Instance.OnHandsWaveLeft -= InstanceOnOnHandsWaveLeft;
    }

    private void InstanceOnOnHandsWaveLeft()
    {
        foreach (var flower in GetComponentsInChildren<Flower>())
        {
            flower.WaveLeft();
        }
    }

    private void InstanceOnOnHandsWaveRight()
    {
        foreach (var flower in GetComponentsInChildren<Flower>())
        {
            flower.WaveRight();
        }
    }


    IEnumerator SpawnFlower()
    {
        float RandomX = Random.Range(CanvaWidth/2, -CanvaWidth/2);
        float RandomY = Random.Range(CanvaHeight/2, -CanvaHeight/2);
        print(RandomX + " " + RandomY);
        GameObject newFlower = Instantiate(Flower,transform);
        Flower flower = newFlower.GetComponent<Flower>();
        flower.Init(new Vector2(RandomX, RandomY),CanvaWidth);
        if (!isSpawningFlower) yield break;
        yield return new WaitForSeconds(flowerSpawnInterval);
        yield return SpawnFlower();
    }
    
    
}
