using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Background object for sound effect script references
    public WitchSFX WitchSFX;

    //The super meter image object
    public Image Meter;

    //Lane movement parameters
    public float moveDistance = 10;
    public int numberOfLanes = 3;
    private int playerPosition = 1;

    //Boolean determining whether to play movement sound
    public bool moveSound = false;

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

    //Boolean to trigger boss phase changes
    public bool bossgothurt = false;

    // Start is called before the first frame update
    void Start()
    {
        //Find the background spawner object to assign the SFX script
        WitchSFX = GameObject.Find("BackgroundSpawner").GetComponent<WitchSFX>();
    }

    //Bomb function; when bomb key is pressed, screen is cleared
    void Bomb()
    {
        //Check if meter is actually full for bomb to go off
        if (meterfill == 100)
        {
            //Empty meter
            meterfill = 10;

            //Start IEnumerator BossHurt to temporarily set boss spawning parameters
            StartCoroutine(BossHurt());

            //Create an array with every enemy on screen
            targets = GameObject.FindGameObjectsWithTag("Enemy");

            //Basic for function to clean out array
            for (var i = 0; i < targets.Length; i++)
            {
                //Kills enemies marked by bomb function (should be all enemies)
                Destroy(targets[i]);
            }

            //Play the sound effect for the bomb
            WitchSFX.BombSFX();
        }
    }

    IEnumerator BossHurt()
    {
        //Set bossgothurt boolean to allow boss phase change
        bossgothurt = true;
        //Wait several seconds
        yield return new WaitForSeconds(3);
        //Reset bossgothurt boolean to prevent bugs with boss spawning
        bossgothurt = false;
    }

    void IncreaseScore()
    {
        //Assign score value based on how close player is to enemy
        //The closer the enemy is, the more "on beat" a dodge is
        //Score and meter values can be changed as needed

        //Each player movement invokes a sound effect as well.
        //Sound effects are assigned based on score gain in the if functions below.

        //No points for moves above a certain distance away, but still sound effects
        if (moveSound)
        {
            if (playermovingup == true)
            {
                WitchSFX.MoveUpSFX();
            }
            if (playermovingdown == true)
            {
                WitchSFX.MoveDownSFX();
            }
        }
        
        print("Distance is: " + distance);
        if (distance < 3 && distance >= 0)
        {
            // distance 3 -> 2^0 = 1
            // distance 2 -> 2^1 = 2
            // distance 1 -> 2^2 = 4
            // distance 0 -> 2^3 = 8
            double exponentialDistance = System.Math.Pow(2, (3 - distance));
            int pointReward = (int)(exponentialDistance * 2.5);
            score += pointReward * 10;
            meterfill += pointReward;
        }

        if (meterfill > 100)
        {
            meterfill = 100;
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

            //Set boolean as true for update function
            playermovingup = true;
            
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

            //Set boolean as true for update function
            playermovingdown = true;
            
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
            print("Bomb");
        }

        // if up key is pressed
        if (Input.GetKeyDown(KeyCode.UpArrow) && playermovingup == false && playermovingdown == false)
        {
            playermovingup = true;
            MoveUp();
        }

        // if down key is pressed
        if (Input.GetKeyDown(KeyCode.DownArrow) && playermovingdown == false && playermovingup == false)
        {
            playermovingdown = true;
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