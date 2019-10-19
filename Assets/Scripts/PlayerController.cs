using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public float moveDistance = 10;
    public int numberOfLanes = 3;
    private int playerPosition = 1;

    // Update is called once per frame
    void Update()
    {
        // if up key is pressed
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (playerPosition < (numberOfLanes - 1))
            {
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
                // move player down on y axis
                transform.Translate(0, -moveDistance, 0);
                // decrement the player lane
                playerPosition--;
            }
        }
    }
}