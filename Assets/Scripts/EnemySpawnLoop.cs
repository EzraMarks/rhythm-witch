using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemySpawnLoop : MonoBehaviour

{
    // Public vars
    public GameObject enemy; // the enemy that this script spawns
    public char enemyIdentifier; // letter (eg 'b') identifying enemy in spawn sequence .txt file
    public String spawnSequenceFilename; // the filename.txt of the spawn sequence
    public float loopDurationInBeats = 80; // number of beats before the sequence loops

    // Arrays containing beat numbers on which to spawn enemy for each lane
    float[] lane1SpawnBeats;
    float[] lane2SpawnBeats;
    float[] lane3SpawnBeats;

    // Index counter variables for each array
    int lane1NextIndex = 0;
    int lane2NextIndex = 0;
    int lane3NextIndex = 0;

    // Transform positions for spawning enemies in lanes
    Transform lane1SpawnTransform;
    Transform lane2SpawnTransform;
    Transform lane3SpawnTransform;

    // Music conductor
    GameObject conductor;
    Composer script;

    // Beat on which the boss loop starts
    float loopStartBeat;


    /* A helper function to skip reading a certain number of lines in the enemy spawn text file
     * Inputs:
     *    reader: the StreamReader for the enemy spawn text file
     *    numLines: the number of lines to skip
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

        StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, spawnSequenceFilename));

        SkipLines(reader, 3);

        String itemStrings;
        for (int lane = 0; lane < 3; lane++)
        {
            itemStrings = reader.ReadLine();
            char[] fields = itemStrings.ToCharArray();

            for (int i = 0; i < fields.Length; i++)
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

    // Start is called before the first frame update
    void Start()
    {
        float[][] enemySequence = ReadFromFile();

        //The level-wide array for this enemy's spawn pattern on lanes 1-3
        //This enemy will spawn a new one at these beats of the song
        lane1SpawnBeats = enemySequence[0]; // lane 1 (top)
        lane2SpawnBeats = enemySequence[1]; // lane 2 (middle)
        lane3SpawnBeats = enemySequence[2]; // lane 3 (bottom)

        GameObject conductor = GameObject.Find("MusicConductor");
        Composer script = conductor.GetComponent<Composer>();

        // Set to beat on which boss sequence started
        loopStartBeat = script.songPositionInBeats;
        // Round beat to the nearest measure
        loopStartBeat = loopStartBeat + 4F - (loopStartBeat % 4F);

    }

    // Update is called once per frame
    void Update()
    {
        if (conductor == null || script == null)
        {
            print("The conductor is not found");

            conductor = GameObject.Find("MusicConductor");
            script = conductor.GetComponent<Composer>();
        }

        if (lane1SpawnTransform == null ||
            lane2SpawnTransform == null ||
            lane3SpawnTransform == null)
        {
            print("The spawn transforms were not found");

            //The lane 1 spawn location for the enemies
            GameObject lane1Spawn = GameObject.Find("Lane 1 Spawn");
            lane1SpawnTransform = lane1Spawn.GetComponent<Transform>();

            //The lane 2 spawn location for the enemies
            GameObject lane2Spawn = GameObject.Find("Lane 2 Spawn");
            lane2SpawnTransform = lane2Spawn.GetComponent<Transform>();

            //The lane 3 spawn location for the enemies
            GameObject lane3Spawn = GameObject.Find("Lane 3 Spawn");
            lane3SpawnTransform = lane3Spawn.GetComponent<Transform>();

        }


        //Check for if current song beat matches the spawn array
        if (lane1NextIndex <= lane1SpawnBeats.Length - 1)
        {
            if (lane1SpawnBeats[lane1NextIndex] <= (script.songPositionInBeats - loopStartBeat))
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
            if (lane2SpawnBeats[lane2NextIndex] <= (script.songPositionInBeats - loopStartBeat))
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
            if (lane3SpawnBeats[lane3NextIndex] <= (script.songPositionInBeats - loopStartBeat))
            {
                //Debug check to make sure spawns are timing properly
                print("Spawn lane 3 confirm");

                //Create new enemy
                Instantiate(enemy, lane3SpawnTransform);

                //Update index counter variable
                lane3NextIndex++;
            }
        }
        // Restart loop once it has ended
        if ((script.songPositionInBeats - loopStartBeat) >= loopDurationInBeats)
        {
            loopStartBeat = loopStartBeat + loopDurationInBeats;
            lane1NextIndex = 0;
            lane2NextIndex = 0;
            lane3NextIndex = 0;
        }
    }
}