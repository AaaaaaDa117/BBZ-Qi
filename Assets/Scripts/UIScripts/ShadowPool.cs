using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPool : MonoBehaviour
{
    public static ShadowPool instance;

    public GameObject shadowPrefab;

    public int shadowCount;
    Queue<GameObject> availableObj = new Queue<GameObject>();
    private void Awake()
    {
        instance = this;
        FillPool();
    }

    public void FillPool()
    {
        for (int i = 0; i < shadowCount; i++)
        {
            var newShadow = Instantiate(shadowPrefab);
            newShadow.transform.SetParent(transform);
            ReturnPool(newShadow);
        }
    }
    public void ReturnPool(GameObject gameObj)
    {
        gameObj.SetActive(false);
        availableObj.Enqueue(gameObj);
    }
    public GameObject GetFromPool()
    {
        if (availableObj.Count == 0)
        {
            FillPool();
        }
        var outShadow = availableObj.Dequeue();
        outShadow.SetActive(true);
        return outShadow;
    }
    private void Update()
    {
        GetFromPool();
    }
}
