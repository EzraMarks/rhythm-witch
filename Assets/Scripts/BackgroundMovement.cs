using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public float backgroundSpeed = 1;
    public float backgroundDespawnTime = 15;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Constant movement to the left
        transform.position += new Vector3(backgroundSpeed * -1 * Time.deltaTime, 0, 0);

        Destroy(gameObject, backgroundDespawnTime);
    }
}