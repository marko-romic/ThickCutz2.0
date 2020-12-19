using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback_Trap : MonoBehaviour
{

    public Rigidbody rb;
    public int knockDistanceModifier = 1;
    public int hp = 10;

    //Setting up trap to spawn on "Frenzy" state
    public GameObject trap;
    public Transform spawner;
    public int trapsPlaced = 0;




    //isKinematic = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            Debug.Log("Monster got knockedback!");
            //isKinematic = false;
            rb.AddForce(-transform.forward * knockDistanceModifier);
            Debug.Log("Lost 1 hp");
            hp -= 1;
        }

        if(hp <= 5)
        {
            if (trapsPlaced <= 3)
            {
                Instantiate(trap, spawner.position, spawner.rotation);
                trapsPlaced += 1;
            }
            Debug.Log("Enemy is in FRENZY mode, it dropped a trap!");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword")
        {
            rb.AddForce(-transform.forward * knockDistanceModifier);
        }
    }
}



