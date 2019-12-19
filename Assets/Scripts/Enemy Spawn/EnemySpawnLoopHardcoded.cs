using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemySpawnLoopHardcoded : MonoBehaviour

{
    // Public vars
    public GameObject enemy; // the enemy that this script spawns
    public char enemyIdentifier; // letter (eg 'b') identifying enemy in spawn sequence .txt file
    public int bossPhaseNumber; // which boss phase is this? 1, 2, or 3.
    public float loopDurationInBeats = 80; // number of beats before the sequence loops
    public String muteMusicObject; // mute this: name of object with audio source containing music
    public String unmuteMusicObject; // unmute this: name of object with audio source containing boss music loop

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





    void setBoss1KnightSpawnSequence()
    {
        lane1SpawnBeats = new float[] { 1.5F, 5.5F, 9.5F, 13.5F, 17.5F, 21.5F, 25.5F, 29.5F, 33.5F, 37.5F, 41.5F, 45.5F, 49.5F, 53.5F };
        lane2SpawnBeats = new float[] { };
        lane3SpawnBeats = new float[] { 1.5F, 5.5F, 9.5F, 13.5F, 17.5F, 21.5F, 25.5F, 29.5F, 33.5F, 37.5F, 41.5F, 45.5F, 49.5F, 53.5F };
    }

    void setBoss1BatSpawnSequence()
    {
        lane1SpawnBeats = new float[] { 3.5F, 11.5F, 12.5F, 15.5F, 23.5F, 24.5F, 27.5F, 35.5F, 36.5F, 39.5F, 47.5F, 48.5F, 50.5F, 52.5F, 56.5F };
        lane2SpawnBeats = new float[] { 3.5F, 7.5F, 10.5F, 12.5F, 15.5F, 19.5F, 22.5F, 24.5F, 27.5F, 31.5F, 34.5F, 36.5F, 39.5F, 43.5F, 46.5F, 48.5F, 51.5F, 54.5F};
        lane3SpawnBeats = new float[] { 7.5F, 10.5F, 11.5F, 19.5F, 22.5F, 23.5F, 31.5F, 34.5F, 35.5F, 43.5F, 46.5F, 47.5F, 50.5F, 52.5F, 54.5F, 55.5F, 56.5F };
    }

    void setBoss2KnightSpawnSequence()
    {
        lane1SpawnBeats = new float[] { 1.5F, 5.5F, 9.5F, 13.5F, 17.5F, 21.5F, 25.5F, 29.5F, 33.5F, 37.5F, 41.5F, 45.5F, 49.5F, 53.5F, 57.5F };
        lane2SpawnBeats = new float[] { };
        lane3SpawnBeats = new float[] { 1.5F, 5.5F, 9.5F, 13.5F, 17.5F, 21.5F, 25.5F, 29.5F, 33.5F, 37.5F, 41.5F, 45.5F, 49.5F, 53.5F, 57.5F };
    }

    void setBoss2BatSpawnSequence()
    {
        lane1SpawnBeats = new float[] { 2.5F, 3.5F, 7.5F, 8.5F, 11.5F, 14.5F, 15.5F, 16.5F, 19.5F, 20.5F, 22.5F, 23.5F, 27.5F, 30.5F, 31.5F, 35.5F, 36.5F, 39.5F, 42.5F, 43.5F, 44.5F, 47.5F, 48.5F, 50.5F, 51.5F, 55.5F };
        lane2SpawnBeats = new float[] { 2.5F, 4.5F, 6.5F, 8.5F, 10.5F, 12.5F, 14.5F, 16.5F, 18.5F, 20.5F, 22.5F, 24.5F, 26.5F, 28.5F, 30.5F, 32.5F, 34.5F, 36.5F, 38.5F, 40.5F, 42.5F, 44.5F, 46.5F, 48.5F, 50.5F, 52.5F, 54.5F, 56.5F };
        lane3SpawnBeats = new float[] { 3.5F, 4.5F, 6.5F, 7.5F, 10.5F, 11.5F, 12.5F, 15.5F, 18.5F, 19.5F, 23.5F, 24.5F, 27.5F, 31.5F, 32.5F, 34.5F, 35.5F, 38.5F, 39.5F, 40.5F, 43.5F, 46.5F, 47.5F, 51.5F, 52.5F, 55.5F };
    }

    void setBoss3KnightSpawnSequence()
    {
        lane1SpawnBeats = new float[] { 1.5F, 5.5F, 9.5F, 13.5F, 17.5F, 21.5F, 25.5F, 29.5F, 33.5F, 41.5F, 49.5F, 53.5F, 57.5F };
        lane2SpawnBeats = new float[] { 37.5F, 45.5F };
        lane3SpawnBeats = new float[] { 1.5F, 5.5F, 9.5F, 13.5F, 17.5F, 21.5F, 25.5F, 29.5F, 33.5F, 37.5F, 41.5F, 45.5F, 49.5F, 53.5F, 57.5F };
    }

    void setBoss3BatSpawnSequence()
    {
        lane1SpawnBeats = new float[] { 2.5F, 3.5F, 6.5F, 7.5F, 10.5F, 11.5F, 14.5F, 15.5F, 18.5F, 19.5F, 22.5F, 23.5F, 26.5F, 27.5F, 30.5F, 31.5F, 34.5F, 35.5F, 36.5F, 38.5F, 40.5F, 42.5F, 43.5F, 44.5F, 46.5F, 48.5F, 51.5F, 55.5F };
        lane2SpawnBeats = new float[] { 3F, 4.5F, 7F, 8.5F, 11F, 12.5F, 15F, 16.5F, 19F, 20.5F, 23F, 24.5F, 27F, 28.5F, 31F, 32.5F, 35F, 36.5F, 38.5F, 39.5F, 41F, 43F, 44.5F, 46.5F, 47.5F, 49F, 50.5F, 52.5F, 54.5F, 56.5F };
        lane3SpawnBeats = new float[] { 2.5F, 3.5F, 6.5F, 7.5F, 10.5F, 11.5F, 14.5F, 15.5F, 18.5F, 19.5F, 22.5F, 23.5F, 26.5F, 27.5F, 30.5F, 31.5F, 34.5F, 35.5F, 39.5F, 40.5F, 42.5F, 43.5F, 47.5F, 48.5F, 51.5F, 55.5F };
    }

    // Start is called before the first frame update
    void Start()
    {
        if (bossPhaseNumber == 1 && enemyIdentifier == 'k')
        {
            setBoss1KnightSpawnSequence();
        }
        else if (bossPhaseNumber == 1 && enemyIdentifier == 'b')
        {
            setBoss1BatSpawnSequence();
        }
        else if (bossPhaseNumber == 2 && enemyIdentifier == 'k')
        {
            setBoss2KnightSpawnSequence();
        }
        else if (bossPhaseNumber == 2 && enemyIdentifier == 'b')
        {
            setBoss2BatSpawnSequence();
        }
        else if (bossPhaseNumber == 3 && enemyIdentifier == 'k')
        {
            setBoss3KnightSpawnSequence();
        }
        else if (bossPhaseNumber == 3 && enemyIdentifier == 'b')
        {
            setBoss3BatSpawnSequence();
        }
        else
        {
            print("Error: invalid enemy identifier (b or k) selected");
        }

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

        //Enable boss loop music
        if (script.songPositionInBeats >= loopStartBeat)
        {
            if (muteMusicObject != "")
            {
                AudioSource muteMusic = GameObject.Find(muteMusicObject).GetComponent<AudioSource>();
                muteMusic.volume = 0F;
            }

            if (unmuteMusicObject != "")
            {
                AudioSource unmuteMusic = GameObject.Find(unmuteMusicObject).GetComponent<AudioSource>();
                unmuteMusic.volume = 1F;
            }
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