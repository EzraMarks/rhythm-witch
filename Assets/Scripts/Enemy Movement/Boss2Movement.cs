using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Movement : MonoBehaviour
{
    public GameObject nextBossPhase;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        if (GameObject.Find("Player").GetComponent<DodgeCollision>().Health > 0)
        {
            // creating a local game object to store the instantiated object and then casting it to a Gamebject
            nextBossPhase = Instantiate(nextBossPhase) as GameObject;

            // Setting the position of the prefab
            nextBossPhase.transform.position =
            new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }
    }
}
