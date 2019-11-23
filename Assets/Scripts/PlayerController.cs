using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Image Meter;

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

    //Boolean for player invincibility during movement
    //This prevents player from being hit in the middle of a lane-change/dodge
    public bool playerinvincible = false;

    //Character "speed" (how fast the movement is)
    float speed = 18;
    //Boolean for if the player is moving up
    bool playermovingup;
    //Boolean for if player is moving down
    bool playermovingdown;
    //Variable to track how far the player has moved during a movement
    float currentmovedistance;
    //Variable for how far along in a movement the player is
    float movementfraction;
    //Vector for the player startpoint in a movement
    Vector3 Startpoint = new Vector3(0, 0, 0);
    //Vector for the player's endpoint during a movement
    Vector3 Endpoint = new Vector3(0, 0, 0);
    //Timer for start of movements
    float timestart;
    //Timer for current duration of movement
    float elapsedtime;
    //Variable for how much distance the player's movement has covered so far
    float distancecovered;

    // Start is called before the first frame update
    void Start()
    {

    }

    //Bomb function; when bomb key is pressed, screen is cleared
    void Bomb()
    {
        //Check if meter is actually full for bomb to go off
        if (meterfill == 100)
        {
            //Empty meter
            meterfill = 0;

            //Create an array with every enemy on screen
            targets = GameObject.FindGameObjectsWithTag("Enemy");

            //Basic for function to clean out array
            for (var i = 0; i < targets.Length; i++)
            {
                //Kills enemies marked by bomb function (should be all enemies)
                Destroy(targets[i]);
            }
        }
    }

    void IncreaseScore()
    {
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
    }

    void MoveUp()
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

            //Score changing function
            IncreaseScore();

            //Once the dodge reward/punishment is calculated, the player moves.
            //This prevents issues with raycasting

            // move player up on y axis
            //transform.Translate(0, moveDistance, 0);
            //Set start and end points for player movement
            Startpoint = transform.position;
            //Debug print
            //print(Startpoint);
            Endpoint = new Vector3(Startpoint.x, Startpoint.y + moveDistance, Startpoint.z);
            //Debug print
            //print(Endpoint);

            //Set start time for movement for interpolation stuff
            timestart = Time.time;

            //Set boolean as true for update function
            playermovingup = true;

            // increment the player lane marker variable
            playerPosition++;

        }
    }

    void MoveDown()
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

            //Score increase function
            IncreaseScore();

            //Once the dodge reward/punishment is calculated, the player moves

            // move player down on y axis
            //transform.Translate(0, -moveDistance, 0);

            //Set start and end points for player movement
            Startpoint = transform.position;
            Endpoint = new Vector3(Startpoint.x, Startpoint.y - moveDistance, Startpoint.z);

            //Set start time for movement for interpolation stuff
            timestart = Time.time;

            //Set boolean as true for update function
            playermovingdown = true;

            // decrement the player lane
            playerPosition--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If right arrow key is pressed, the bomb goes off
        if (Input.GetKeyDown (KeyCode.RightArrow))
        {
            Bomb();
            //Debug print
            //print("Bomb");
        }
        // if up key is pressed
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
        }
        // if down key is pressed
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }

        //PLAYER MOVING UP
        //Check if the player is currently supposed to be moving up
        if (playermovingup == true)
        {
            //Set player invincibility during movement
            playerinvincible = true;
            //Set the current distance traversed by the player
            //currentmovedistance = transform.position.y - Startpoint.y;
            //Find how long the movement has gone for
            elapsedtime = Time.time - timestart;
            //Calculate how far along the movement the player is
            //movementfraction = currentmovedistance / moveDistance;
            //Determine how far along the movement is
            distancecovered = elapsedtime * speed;
            //Debug prints
            //print(distancecovered);
            //print(Startpoint);
            //print(Endpoint);
            //Interpolate and update the player's movement appropriately
            transform.position = Vector3.Lerp(Startpoint, Endpoint, distancecovered);

            //Check if the player has completed their movement
            if (distancecovered >= 1)
            {
                //Set final position of movement
                transform.position = new Vector3(Startpoint.x, Startpoint.y + moveDistance, Startpoint.z);
                //Close out the function by setting booleans to false
                playerinvincible = false;
                playermovingup = false;
            }
        }

        //PLAYER MOVING DOWN
        //Check if the player is currently supposed to be moving down
        if (playermovingdown == true)
        {
            //Set player invincibility during movement
            playerinvincible = true;
            //Set the current distance traversed by the player
            //currentmovedistance = transform.position.y - Startpoint.y;
            //Find how long the movement has gone for
            elapsedtime = Time.time - timestart;
            //Calculate how far along the movement the player is
            //movementfraction = currentmovedistance / moveDistance;
            //Determine how far along the movement is
            distancecovered = elapsedtime * speed;
            //Debug prints
            //print(distancecovered);
            //print(Startpoint);
            //print(Endpoint);
            //Interpolate and update the player's movement appropriately
            transform.position = Vector3.Lerp(Startpoint, Endpoint, distancecovered);

            //Check if the player has completed their movement
            if (distancecovered >= 1)
            {
                //Set final position of movement
                transform.position = new Vector3(Startpoint.x, Startpoint.y - moveDistance, Startpoint.z);
                //Close out the function by setting booleans to false
                playerinvincible = false;
                playermovingdown = false;
            }
        }

        Meter.fillAmount = meterfill / 100;

    }
}