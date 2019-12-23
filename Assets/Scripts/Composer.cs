using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Composer : MonoBehaviour
{
    //Song beats per minute
    public float songBPM;

    //Number of seconds per song beat
    public float secPerBeat;

    //Current song position in seconds
    public float songPosition;

    //Current song position in beats
    public float songPositionInBeats;

    //Total time in seconds since song started
    public float dspSongTime;

    //audio source attached to object to play music
    public AudioSource musicSource;

    //Adjustable offset for first beat of song
    public float firstBeatOffset;

    //Boolean that keeps beat counter from starting until song starts
    bool songstarted;

    // Start is called before the first frame update
    void Start()
    {
        //Keeps update function from running until it's time
        songstarted = false;
        
        //Load the AudioSource attached to the Conductor
        musicSource = GetComponent<AudioSource>();

        //Coroutine that starts the song after it's had time to load properly
        StartCoroutine(SongStart());
    }

    IEnumerator SongStart()
    {
        //Waits to let song load before playing
        yield return new WaitForSeconds(2);
        
        //Find seconds per song beat
        secPerBeat = 60f / songBPM;

        //Record when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        //Start playing the music
        musicSource.Play();

        //Gives the go-ahead to the update function
        songstarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if function keeps game from counting beats until song starts, to prevent bugs
        if (songstarted == true)
        {
            //find how many seconds since the song started
            songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

            //find how many beats since the song started
            songPositionInBeats = songPosition / secPerBeat;
        }
    }
}
