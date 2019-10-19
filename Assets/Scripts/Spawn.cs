using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour

{

    //Basic variable declarations for void Start()
    float[] lane1SpawnBeats;
    float[] lane2SpawnBeats;
    float[] lane3SpawnBeats;
    int lane1NextIndex;
    int lane2NextIndex;
    int lane3NextIndex;

    //Enemy object and lane position declarations
    public GameObject Enemy;
    public Transform Lane1SpawnTransform;
    public Transform Lane2SpawnTransform;
    public Transform Lane3SpawnTransform;

    // Start is called before the first frame update
    void Start()
    {

        //TESTTESTTEST
        //Testing if prefab instantiation works
        //Instantiate(Enemy, new Vector3(0,0,0), Quaternion.identity);

        //The level-wide array for this enemy's spawn pattern on lane 1 (top lane)
        //This enemy will spawn a new one at these beats of the song
        lane1SpawnBeats = new float[2] { 3, 12 };

        //The level-wide array for this enemy's spawn pattern on lane 2 (middle lane)
        //This enemy will spawn a new one at these beats of the song
        lane2SpawnBeats = new float[2] { 6, 9 };

        //The level-wide array for this enemy's spawn pattern on lane 3 (bottom lane)
        //This enemy will spawn a new one at these beats of the song
        lane3SpawnBeats = new float[2] { 9, 12 };

        //Index counter variables for each array
        lane1NextIndex = 0;
        lane2NextIndex = 0;
        lane3NextIndex = 0;

        //The lane 1 spawn location for the enemies
        GameObject Lane1Spawn = GameObject.Find("Lane 1 Spawn");
        Transform Lane1SpawnTransform = Lane1Spawn.GetComponent<Transform>();

        //The lane 2 spawn location for the enemies
        GameObject Lane2Spawn = GameObject.Find("Lane 2 Spawn");
        Transform Lane2SpawnTransform = Lane2Spawn.GetComponent<Transform>();

        //The lane 3 spawn location for the enemies
        GameObject Lane3Spawn = GameObject.Find("Lane 3 Spawn");
        Transform Lane3SpawnTransform = Lane3Spawn.GetComponent<Transform>();

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
                print("Spawn Lane 1 confirm");

                //Create new enemy
                Instantiate(Enemy, Lane1SpawnTransform);


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
                print("Spawn Lane 2 confirm");

                //Create new enemy
                Instantiate(Enemy, Lane2SpawnTransform);

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
                print("Spawn Lane 3 confirm");

                //Create new enemy
                Instantiate(Enemy, Lane3SpawnTransform);

                //Update index counter variable
                lane3NextIndex++;
            }
        }
    }
}