﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMovement : MonoBehaviour
{
    //Knight enemy speed value; higher is faster (should only tweak to match BPM)
    //Goal is for enemies to go from spawn to collision range in an exact number of beats; 6, 8, 10, etc.
    //Change this speed value so that Knight enemy reaches player in desired beat number
    public float KnightSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Constant movement to the left, starting from spawn
        //Can modify with more lines/scripts later to match movement to beat cosmetically
        transform.position += new Vector3(KnightSpeed * -1 * Time.deltaTime, 0, 0);
    }
}
