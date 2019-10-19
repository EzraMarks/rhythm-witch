using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMovement : MonoBehaviour
{

    public float KnightSpeed;

    // Start is called before the first frame update
    void Start()
    {
        KnightSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(KnightSpeed * -1 * Time.deltaTime, 0, 0);
    }
}
