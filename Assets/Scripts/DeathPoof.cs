using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPoof : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
