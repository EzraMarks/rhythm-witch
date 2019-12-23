using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemySpawnHardcoded : MonoBehaviour

{

    //The level-wide array for this enemy's spawn pattern on lane 1 (top lane)
    //This enemy will spawn a new one at these beats of the song
    float[] lane1SpawnBeats;
    //The level-wide array for this enemy's spawn pattern on lane 1 (middle lane)
    //This enemy will spawn a new one at these beats of the song
    float[] lane2SpawnBeats;
    //The level-wide array for this enemy's spawn pattern on lane 1 (bottom lane)
    //This enemy will spawn a new one at these beats of the song
    float[] lane3SpawnBeats;

    int lane1NextIndex;
    int lane2NextIndex;
    int lane3NextIndex;

    //enemy object and lane position declarations
    public GameObject enemy;
    public Transform lane1SpawnTransform;
    public Transform lane2SpawnTransform;
    public Transform lane3SpawnTransform;

    // "k" to spawn knights, "b" to spawn bats
    public char enemyIdentifier;

    void setBatSpawnSequence()
    {
        lane1SpawnBeats = new float[] { 18.5F, 26.5F, 28.5F, 36.5F, 39.5F, 46.5F, 48.5F, 50.5F, 54.5F, 58.5F, 59.5F, 60.5F, 63.5F, 68.5F, 80.5F, 83.5F, 88.5F, 91.5F, 92.5F, 94.5F, 99.5F, 100.5F, 102.5F, 106.5F, 108.5F, 111.5F, 115.5F, 118.5F, 120.5F, 122.5F, 123.5F, 124.5F, 126.5F, 129.5F, 131F, 132.5F, 134.5F, 136.5F, 137.5F, 139F, 140.5F, 142.5F, 143.5F, 145.5F, 147F, 148.5F, 151F, 158.5F, 159.5F, 160.5F, 162.5F, 163.5F, 164.5F, 166.5F, 167.5F, 168.5F, 170.5F, 171.5F, 172.5F, 174.5F, 176.5F, 178.5F, 180.5F, 182.5F, 184F, 186.5F, 188F };
        lane2SpawnBeats = new float[] { 26.5F, 28.5F, 34.5F, 43.5F, 47.5F, 51.5F, 55.5F, 56.5F, 64.5F, 67.5F, 71.5F, 72.5F, 75.5F, 76.5F, 79.5F, 84.5F, 87.5F, 90.5F, 92.5F, 95.5F, 96.5F, 98.5F, 103.5F, 104.5F, 106.5F, 107.5F, 110.5F, 112.5F, 115.5F, 119.5F, 123.5F, 127.5F, 128F, 128.5F, 131F, 135.5F, 137.5F, 138F, 138.5F, 139F, 142.5F, 144.5F, 147F, 147.5F, 149.5F, 159.5F, 163.5F, 167.5F, 171.5F, 175F, 179F, 183F, 184.5F, 187F, 188.5F };
        lane3SpawnBeats = new float[] { 18.5F, 34.5F, 36.5F, 39.5F, 43.5F, 47.5F, 51.5F, 54.5F, 58.5F, 59.5F, 60.5F, 63.5F, 71.5F, 80.5F, 83.5F, 84.5F, 88.5F, 90.5F, 91.5F, 94.5F, 99.5F, 100.5F, 102.5F, 108.5F, 111.5F, 114.5F, 116.5F, 118.5F, 119.5F, 120.5F, 122.5F, 124.5F, 126.5F, 127.5F, 129.5F, 130F, 132.5F, 134.5F, 135.5F, 136.5F, 140.5F, 143.5F, 144.5F, 145.5F, 148.5F, 149.5F, 151F, 158.5F, 160.5F, 162.5F, 164.5F, 166.5F, 168.5F, 170.5F, 172.5F, 174.5F, 175F, 176.5F, 178.5F, 179F, 180.5F, 182.5F, 183F, 184F, 184.5F, 186.5F, 187F, 188F, 188.5F };
    }

    void setKnightSpawnSequence()
    {
        lane1SpawnBeats = new float[] { 29.5F, 41.5F, 57.5F, 73.5F, 77.5F, 93.5F, 97.5F, 101.5F, 105.5F, 141.5F };
        lane2SpawnBeats = new float[] { 13.5F, 21.5F, 37.5F, 41.5F, 45.5F, 49.5F, 53.5F, 61.5F, 69.5F, 81.5F, 85.5F, 89.5F, 93.5F, 101.5F, 109.5F, 113.5F, 117.5F, 121.5F, 125.5F, 133.5F, 157.5F, 161.5F, 165.5F, 169.5F, 173.5F, 177.5F, 181.5F, 185.5F };
        lane3SpawnBeats = new float[] { 21.5F, 29.5F, 45.5F, 49.5F, 57.5F, 65.5F, 73.5F, 77.5F, 81.5F, 85.5F, 89.5F, 97.5F, 105.5F, 109.5F, 113.5F, 117.5F, 121.5F, 125.5F, 133.5F, 141.5F, 157.5F, 161.5F, 165.5F, 169.5F, 173.5F, 177.5F, 181.5F, 185.5F };
    }

    // Start is called before the first frame update
    void Start()
    {
        if (enemyIdentifier == 'k')
        {
            setKnightSpawnSequence();
        } else if (enemyIdentifier == 'b')
        {
            setBatSpawnSequence();
        }
        else
        {
            print("Error: invalid enemy identifier (b or k) selected");
        }

        //Index counter variables for each array
        lane1NextIndex = 0;
        lane2NextIndex = 0;
        lane3NextIndex = 0;

        //The lane 1 spawn location for the enemies
        GameObject lane1Spawn = GameObject.Find("Lane 1 Spawn");
        Transform lane1SpawnTransform = lane1Spawn.GetComponent<Transform>();

        //The lane 2 spawn location for the enemies
        GameObject lane2Spawn = GameObject.Find("Lane 2 Spawn");
        Transform lane2SpawnTransform = lane2Spawn.GetComponent<Transform>();

        //The lane 3 spawn location for the enemies
        GameObject lane3Spawn = GameObject.Find("Lane 3 Spawn");
        Transform lane3SpawnTransform = lane3Spawn.GetComponent<Transform>();

        //Call conductor object for song position
        GameObject conductor = GameObject.Find("MusicConductor");
        Composer script = conductor.GetComponent<Composer>();

    }

    // Update is called once per frame
    void Update()
    {
        //Debug check for beats
        //print(GameObject.Find("MusicConductor").GetComponent<Composer>().songPositionInBeats);

        //Check for if current song beat matches the spawn array
        if (lane1NextIndex <= lane1SpawnBeats.Length - 1)
        {
            if (lane1SpawnBeats[lane1NextIndex] <= GameObject.Find("MusicConductor").GetComponent<Composer>().songPositionInBeats)
            {
                //Debug check to make sure spawns are timing properly
                //print("Spawn lane 1 confirm");

                //Create new enemy
                Instantiate(enemy, lane1SpawnTransform);


                //Update index counter variable
                lane1NextIndex++;
            }
        }
        //Check for if current song beat matches the spawn array
        if (lane2NextIndex <= lane2SpawnBeats.Length - 1)
        {
            if (lane2SpawnBeats[lane2NextIndex] <= GameObject.Find("MusicConductor").GetComponent<Composer>().songPositionInBeats)
            {
                //Debug check to make sure spawns are timing properly
                //print("Spawn lane 2 confirm");

                //Create new enemy
                Instantiate(enemy, lane2SpawnTransform);

                //Update index counter variable
                lane2NextIndex++;
            }
        }
        //Check for if current song beat matches the spawn array
        if (lane3NextIndex <= lane3SpawnBeats.Length - 1)
        {
            if (lane3SpawnBeats[lane3NextIndex] <= GameObject.Find("MusicConductor").GetComponent<Composer>().songPositionInBeats)
            {
                //Debug check to make sure spawns are timing properly
                //print("Spawn lane 3 confirm");

                //Create new enemy
                Instantiate(enemy, lane3SpawnTransform);

                //Update index counter variable
                lane3NextIndex++;
            }
        }
    }
}