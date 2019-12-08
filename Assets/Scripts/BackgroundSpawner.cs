using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{

    private double nextActionTime = 0.0;
    public double backgroundSpawnPeriod = 1;

    public GameObject layer;
    bool selfDestructing = false;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("BackgroundSpawner").Length > 1)
        {
            // destroy self
            selfDestructing = true;
            Destroy(gameObject);
        }

        if (!selfDestructing)
        {
            nextActionTime += backgroundSpawnPeriod;
            Instantiate(layer);
        }
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