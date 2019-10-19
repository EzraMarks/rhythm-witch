using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeCollision : MonoBehaviour
{

    //Player health; this number of hits results in the player losing
    public float Health;

    // Start is called before the first frame update
    void Start()
    {
        //Set player health value
        Health = 3;   
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

        //Raycast to check how close the next enemy in this lane is
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right);

        //When the raycast finds an enemy
        if (hit.collider != null)
        {
            float distance = Mathf.Abs(hit.point.x - transform.position.x);
            print (distance);
        }
        if (hit.collider == null)
        {
            print("null");
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
        GameObject.Destroy(coll.gameObject);
    }
}
