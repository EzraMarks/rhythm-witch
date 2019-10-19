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

    // Start is called before the first frame update
    void Start()
    {
        //Load the AudioSource attached to the Conductor
        musicSource = GetComponent<AudioSource>();

        //Find seconds per song beat
        secPerBeat = 60f / songBPM;

        //Record when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        //Start playing the music
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //find how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

        //find how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;
    }
}
