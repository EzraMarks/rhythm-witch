using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    //Set up the sprite array used to pull sprites for the health bar animation
    //Includes sprites for every health state: 3, 2, and 1 health
    //0-3 are 1 health, 4-7 are 2 health, 8-11 are full health
    public Sprite[] frameReservoir;


    
    // Start is called before the first frame update
    void Start()
    {
        //Gif Animator script, so that animation can be changed
        GifAnimator GifAnimator = GetComponent<GifAnimator>();

        //Assign frames for full health bar
        GifAnimator.frames[0] = frameReservoir[16];
        GifAnimator.frames[1] = frameReservoir[17];
        GifAnimator.frames[2] = frameReservoir[18];
        GifAnimator.frames[3] = frameReservoir[19];
        GifAnimator.frames[4] = frameReservoir[20];
        GifAnimator.frames[5] = frameReservoir[21];
        GifAnimator.frames[6] = frameReservoir[22];
        GifAnimator.frames[7] = frameReservoir[23];

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Function that changes health bar on screen
    public void HealthBarDamage()
    {

        //For losing the first heart
        if (GameObject.Find("Player").GetComponent<DodgeCollision>().Health == 2)
        {
            //Gif Animator script, so that animation can be changed
            GifAnimator GifAnimator = GetComponent<GifAnimator>();

            //Assign frames for health bar with two hearts
            GifAnimator.frames[0] = frameReservoir[8];
            GifAnimator.frames[1] = frameReservoir[9];
            GifAnimator.frames[2] = frameReservoir[10];
            GifAnimator.frames[3] = frameReservoir[11];
            GifAnimator.frames[4] = frameReservoir[12];
            GifAnimator.frames[5] = frameReservoir[13];
            GifAnimator.frames[6] = frameReservoir[14];
            GifAnimator.frames[7] = frameReservoir[15];
        }

        //For losing the second heart
        if (GameObject.Find("Player").GetComponent<DodgeCollision>().Health == 1)
        {
            //Gif Animator script, so that animation can be changed
            GifAnimator GifAnimator = GetComponent<GifAnimator>();

            //Assign frames for health bar with two hearts
            GifAnimator.frames[0] = frameReservoir[0];
            GifAnimator.frames[1] = frameReservoir[1];
            GifAnimator.frames[2] = frameReservoir[2];
            GifAnimator.frames[3] = frameReservoir[3];
            GifAnimator.frames[4] = frameReservoir[4];
            GifAnimator.frames[5] = frameReservoir[5];
            GifAnimator.frames[6] = frameReservoir[6];
            GifAnimator.frames[7] = frameReservoir[7];
        }

        //For losing the final heart and losing
        if (GameObject.Find("Player").GetComponent<DodgeCollision>().Health == 0)
        {
            //Gif Animator script, so that animation can be changed
            GifAnimator GifAnimator = GetComponent<GifAnimator>();

            //Assign frames for health bar with two hearts
            GifAnimator.frames[0] = frameReservoir[24];
            GifAnimator.frames[1] = frameReservoir[24];
            GifAnimator.frames[2] = frameReservoir[24];
            GifAnimator.frames[3] = frameReservoir[24];
            GifAnimator.frames[4] = frameReservoir[24];
            GifAnimator.frames[5] = frameReservoir[24];
            GifAnimator.frames[6] = frameReservoir[24];
            GifAnimator.frames[7] = frameReservoir[24];
        }
    }
}
