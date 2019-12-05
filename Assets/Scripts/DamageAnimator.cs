using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAnimator : MonoBehaviour
{
    //Array to store sprites for animation
    public Sprite[] frames;

    //Frames per second value (higher = faster animation)
    public double framesPerSecond = 10.0;

    //Sprite Renderer Object on Player
    SpriteRenderer spriteRenderer;

    //Boolean for update function (whether or not to run animation)
    bool damage;

    //Damage boost invincibility (different from regular invincibility to prevent boolean issues in future)
    public bool playerdamageboost;

    // Start is called before the first frame update
    void Start()
    {
        //Get renderer on player object
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Make animation not run at start
        spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Only runs animation when damaged
        if (damage == true)
        {
            //Turns on animation
            spriteRenderer.enabled = true;
            //Runs animation at given speed
            int index = (int)(Time.time * framesPerSecond) % frames.Length;
            spriteRenderer.sprite = frames[index];
        }
        else
        {
            //Keeps animation turned off when not wanted
            spriteRenderer.enabled = false;
        }
    }

    public void DamageTrigger()
    {
        StartCoroutine(DamageAnimation());
    }

    public IEnumerator DamageAnimation()
    {
        damage = true;
        playerdamageboost = true;
        yield return new WaitForSeconds(1);
        damage = false;
        playerdamageboost = false;
    }
}