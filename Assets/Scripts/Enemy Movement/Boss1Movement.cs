using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Movement : MonoBehaviour
{
    //Object to replace this one for next boss phase
    public GameObject nextBossPhase;
    //Boolean to determine whether to go to next boss phase
    bool bossgothurt;

    // Start is called before the first frame update
    void Start()
    {
        //Find bool bossgothurt in relevant script
        bool bossgothurt = GameObject.Find("Player").GetComponent<PlayerController>().bossgothurt;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //When destroyed by player bomb
    private void OnDestroy()
    {
        //Update boolean
        bool bossgothurt = GameObject.Find("Player").GetComponent<PlayerController>().bossgothurt;
        //If boolean triggers next boss phase
        if (bossgothurt == true)
        {
            // creating a local game object to store the instantiated object and then casting it to a Gamebject
            nextBossPhase = Instantiate(nextBossPhase) as GameObject;

            // Setting the position of the prefab
            nextBossPhase.transform.position =
            new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }

    }
}
