using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyDrone : MonoBehaviour
{

    public Rigidbody rb;
    public int hp = 10;

    //Setting up trap to spawn on "Frenzy" state
    public GameObject Ally;
    public Transform spawn;
    public int trapsPlaced = 0;

    //Sets up ally and makes sure you can only have one
    public int ally = 0;

    //isKinematic = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("I"))
        {
            Debug.Log("Would you like to spawn a police drone? Y/N");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (ally == 0)
            {
                Instantiate(Ally, spawn.position, spawn.rotation);
                ally += 1;
            }
        }
    }
}



