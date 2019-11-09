using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{

    private double nextActionTime = 0.0;
    public double backgroundSpawnPeriod = 1;

    public GameObject layer;

    // Start is called before the first frame update
    void Start()
    {
        nextActionTime += backgroundSpawnPeriod;
        Instantiate(layer);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += backgroundSpawnPeriod;
            Instantiate(layer);
        }
    }
}