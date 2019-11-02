using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Image Meter;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    public float moveDistance = 10;
    public int numberOfLanes = 3;
    private int playerPosition = 1;

    //The distance between the player and the nearest enemy
    //This is used to determine meter fill and/or score
    float distance;
    //Score value
    public float score;
    //Meter fill value (out of 100)
    public float meterfill;
    //"targets" is an array to store all on-screen enemies
    //This way, the "bomb" can destroy them all at once
    GameObject[] targets;
    

    // Update is called once per frame
    void Update()
    {
        //If control is pressed, the bomb goes off
        if (Input.GetKeyDown (KeyCode.RightControl))
        {
            if (meterfill == 100)
            {
                meterfill = 0;
                targets = GameObject.FindGameObjectsWithTag("Enemy");

                for (var i = 0; i < targets.Length; i++)
                {
                    Destroy(targets[i]);
                }
            }
        }
        // if up key is pressed
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (playerPosition < (numberOfLanes - 1))
            {
                //Raycast to check how close the next enemy in this lane is.
                //Ray starts from center of player and ignores the player's collider box,
                //but ONLY IF "QUERIES START IN COLLIDER" IS TURNED OFF IN SETTINGS
                //(Edit -> Project Settings -> Physics 2D -> Queries start in colliders)

                //Also, raycast starts from offset position based on collider size.
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right);

                //Measure how far away the enemy is from the player
                distance = Mathf.Abs(hit.point.x - transform.position.x);

                //Assign score value based on how close player is to enemy
                //The closer the enemy is, the more "on beat" a dodge is
                //Score and meter values can be changed as needed
                if (distance < 1.2 && distance >= 1)
                {
                    score += 100;
                    meterfill += 20;
                    if (meterfill > 100)
                    {
                        meterfill = 100;
                    }
                }
                if (distance < 1 && distance >= 0.8)
                {
                    score += 300;
                    meterfill += 30;
                    if (meterfill > 100)
                    {
                        meterfill = 100;
                    }
                }
                if (distance < 0.8 && distance >= 0.7)
                {
                    score += 500;
                    meterfill += 40;
                    if (meterfill > 100)
                    {
                        meterfill = 100;
                    }
                }
                if (distance < 0.7 && distance >= 0.6)
                {
                    score += 1000;
                    meterfill += 50;
                    if (meterfill > 100)
                    {
                        meterfill = 100;
                    }
                }

                //Once the dodge reward/punishment is calculated, the player moves
                // move player up on y axis
                transform.Translate(0, moveDistance, 0);
                // increment the player lane
                playerPosition++;

            }
        }
        // if down key is pressed
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (playerPosition > 0)
            {
                //Raycast to check how close the next enemy in this lane is.
                //Ray starts from center of player and ignores the player's collider box,
                //but ONLY IF "QUERIES START IN COLLIDER" IS TURNED OFF IN SETTINGS
                //(Edit -> Project Settings -> Physics 2D -> Queries start in colliders)

                //Also, raycast starts from offset position based on collider size.
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right);

                //Measure how far away the enemy is from the player
                distance = Mathf.Abs(hit.point.x - transform.position.x);

                //Assign score value based on how close player is to enemy
                //The closer the enemy is, the more "on beat" a dodge is
                //Score and meter values can be changed as needed
                if (distance < 1.2 && distance >= 1)
                {
                    score += 100;
                    meterfill += 1;
                }
                if (distance < 1 && distance >= 0.8)
                {
                    score += 300;
                    meterfill += 3;
                }
                if (distance < 0.8 && distance >= 0.7)
                {
                    score += 500;
                    meterfill += 5;
                }
                if (distance < 0.7 && distance >= 0.6)
                {
                    score += 1000;
                    meterfill += 10;
                }

                //Once the dodge reward/punishment is calculated, the player moves
                // move player down on y axis
                transform.Translate(0, -moveDistance, 0);
                // decrement the player lane
                playerPosition--;
            }
        }

        Meter.fillAmount = meterfill / 100;

    }
}