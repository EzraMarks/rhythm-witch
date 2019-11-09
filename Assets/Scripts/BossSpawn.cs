using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    // beat on which to spawn the boss
    public int bossSpawnBeat = 5;

    // whether the boss has been spawned yet
    bool bossSpawned = false;

    //enemy object and lane position declarations
    public GameObject boss;
    public Transform laneSpawnTransform;

    // Start is called before the first frame update
    void Start()
    {
        //Call conductor object for song position
        GameObject conductor = GameObject.Find("MusicConductor");
        Composer script = conductor.GetComponent<Composer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!bossSpawned && bossSpawnBeat <= GameObject.Find("MusicConductor").GetComponent<Composer>().songPositionInBeats)
        {
            // spawn the boss
            Instantiate(boss, laneSpawnTransform);

            bossSpawned = true;
        }
    }
}
