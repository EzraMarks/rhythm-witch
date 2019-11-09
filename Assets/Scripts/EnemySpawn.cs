using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemySpawn : MonoBehaviour

{

    //Basic variable declarations for void Start()
    float[] lane1SpawnBeats;
    float[] lane2SpawnBeats;
    float[] lane3SpawnBeats;
    int lane1NextIndex;
    int lane2NextIndex;
    int lane3NextIndex;

    //enemy object and lane position declarations
    public GameObject enemy;
    public Transform lane1SpawnTransform;
    public Transform lane2SpawnTransform;
    public Transform lane3SpawnTransform;

    // a letter e.g. "k" or "b" which identifies this enemy in the spawn sequence file
    public string enemyIdentifier;

    // ReadFromFile reads in the enemy spawn sequence from a text file
        // path for the file containing the enemy spawning sequence, e.g.:
        // b0 b19 k20 <-- lane 1
        // b0 k19 b30 <-- lane 2
        // b3 k10 k30 <-- lane 3
    float[][] ReadFromFile()
    {
        List<float>[] enemySpawnSequence = { new List<float>(), new List<float>(), new List<float>() };

        StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "enemySpawnSequence.txt"));

        String itemStrings;
        char[] delimiter = { ' ' };

        for (int lane = 0; lane < 3; lane++)
        {
            itemStrings = reader.ReadLine();
            string[] fields = itemStrings.Split(delimiter);

            for (int i = 0; i < fields.Length; i++)
            {
                if (fields[i].StartsWith(enemyIdentifier))
                {
                    string intString = fields[i].Substring(1);
                    float beatNum = 0;
                    if (!float.TryParse(intString, out beatNum))
                    {
                        beatNum = 0; // TODO improve error condition -- failures result in enemy spawn on beat 0
                    }
                    enemySpawnSequence[lane].Add(beatNum);
                }
            }
        }

        float[][] enemySpawnSequenceArray = { enemySpawnSequence[0].ToArray(), enemySpawnSequence[1].ToArray(), enemySpawnSequence[2].ToArray() };
        return enemySpawnSequenceArray;
    }

    // Start is called before the first frame update
    void Start()
    {
        float[][] enemySequence = ReadFromFile();

        //TESTTESTTEST
        //Testing if prefab instantiation works
        //Instantiate(enemy, new Vector3(0,0,0), Quaternion.identity);

        //The level-wide array for this enemy's spawn pattern on lane 1 (top lane)
        //This enemy will spawn a new one at these beats of the song
        lane1SpawnBeats = enemySequence[0];

        //The level-wide array for this enemy's spawn pattern on lane 2 (middle lane)
        //This enemy will spawn a new one at these beats of the song
        lane2SpawnBeats = enemySequence[1];

        //The level-wide array for this enemy's spawn pattern on lane 3 (bottom lane)
        //This enemy will spawn a new one at these beats of the song
        lane3SpawnBeats = enemySequence[2];

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
                print("Spawn lane 1 confirm");

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
                print("Spawn lane 2 confirm");

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
                print("Spawn lane 3 confirm");

                //Create new enemy
                Instantiate(enemy, lane3SpawnTransform);

                //Update index counter variable
                lane3NextIndex++;
            }
        }
    }
}