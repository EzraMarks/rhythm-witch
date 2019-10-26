using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeCollision : MonoBehaviour
{

    //Player health; this number of hits results in the player losing
    public float Health;

    //Distance between player and nearest enemy on the same lane
    public float distance;

    //Collider for transform math in raycast section of Update()
    //Collider2D playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        //Set player health value
        Health = 3;

        //Get collider for transform math later
        //Collider2D playerCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check for it the player has run out of health
        if (Health == 0)
        {
            //Debug message for player losing the game (placeholder until death is fleshed out)
            print("The Player has lost the game.");

            //Change health to prevent thousands of prints
            Health--;
        }

        //Raycast to check how close the next enemy in this lane is.
        //Ray starts from center of player and ignores the player's collider box,
        //but ONLY IF "QUERIES START IN COLLIDER" IS TURNED OFF IN SETTINGS
        //(Edit -> Project Settings -> Physics 2D -> Queries start in colliders)

        //Also, raycast starts from offset position based on collider size.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right);

        //When the raycast finds an enemy
        if (hit.collider != null)
        {
            //Measure how far away the enemy is from the player
            distance = Mathf.Abs(hit.point.x - transform.position.x);
            
            //Debug distance measurement
            //print (distance);
        }

        //Debug check for when raycast hits nothing
        if (hit.collider == null)
        {
            //Debug message for no raycast hit
            print("null");

            //Set distance value arbitrarily high to guarantee no points/meter
            distance = 20;
        }

    }

    //If any colliders (since they're all enemies right now) bump against the player,
    //the player takes damage
    void OnTriggerEnter2D(Collider2D coll)
    {
        //Debug message for player taking damage
        print("Taken 1 damage");

        //Player loses one health
        Health--;

        //Enemy is destroyed on impact
        //This can be changed in future depending on artistic decisions
        GameObject.Destroy(coll.gameObject);
    }
}
