﻿using System;
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
    public char enemyIdentifier;

    /* A helper function to skip reading a certain number of lines in the enemy spawn text file
     * Inputs:
     * reader: the StreamReader for the enemy spawn text file
     * numLines: the number of lines to skip
     */ 
    void SkipLines(StreamReader reader, int numLines)
    {
        for (int i = 0; i < numLines; i++) // skip over numLines of text from file
        {
            reader.ReadLine();
        }
    }
    
    // ReadFromFile reads in the enemy spawn sequence from a text file
    float[][] ReadFromFile()
    {
        List<float>[] enemySpawnSequence = { new List<float>(), new List<float>(), new List<float>() };

        StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "EnemySpawnSequence.txt"));

        SkipLines(reader, 3);

        String itemStrings;
        for (int lane = 0; lane < 3; lane++)
        {
            itemStrings = reader.ReadLine();
            char[] fields = itemStrings.ToCharArray();

            for (int i = 2; i < fields.Length; i++)
            {
                if (fields[i] == enemyIdentifier)
                {
                    enemySpawnSequence[lane].Add((float)i / 2);
                }
            }
        }


        float[][] enemySpawnSequenceArray = { enemySpawnSequence[0].ToArray(), enemySpawnSequence[1].ToArray(), enemySpawnSequence[2].ToArray() };
        return enemySpawnSequenceArray;
    }
    // Print the array created from the spawn sequence file to the console
    void PrintFromFile()
    {
        string printString = "";
        for (int i = 0; i < lane1SpawnBeats.Length; i++)
        {
            printString = printString + lane1SpawnBeats[i] + "F, ";
        }
        print(enemyIdentifier + "1: " + printString);

        printString = "";
        for (int i = 0; i < lane2SpawnBeats.Length; i++)
        {
            printString = printString + lane2SpawnBeats[i] + "F, ";
        }
        print(enemyIdentifier + "2: " + printString);

        printString = "";
        for (int i = 0; i < lane3SpawnBeats.Length; i++)
        {
            printString = printString + lane3SpawnBeats[i] + "F, ";
        }
        print(enemyIdentifier + "3: " + printString);
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