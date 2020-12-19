using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public Transform player;
    public Animator anim;
    public Rigidbody eRb;
    private Vector3 moveDirection;



    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        eRb = GetComponent<Rigidbody>();

        eRb.isKinematic = true;
    }

    private void FixedUpdate()
    {
        eRb.AddForce(moveDirection * 50f); //movement force 
    }

    void Update()
    {
        moveDirection = Vector3.zero; //set to 0 at the start

        if (Vector3.Distance(player.position, this.transform.position) < 5)
        {

            if (Vector3.Distance(player.position, this.transform.position) < 5)
            {
                Vector3 direction = player.position - this.transform.position;
                direction.y = 0;

                this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                            Quaternion.LookRotation(direction), 0.1f);

                anim.SetBool("IsIdle", false);
                if (direction.magnitude > .8)
                {
                    moveDirection = direction.normalized;
                    anim.SetBool("IsWalking", true);
                    anim.SetBool("IsAttacking", false);
                }
                else
                {
                    anim.SetBool("IsAttacking", true);
                    anim.SetBool("IsWalking", false);
                }
            }
            else
            {
                anim.SetBool("IsIdle", true);
                anim.SetBool("IsWalking", false);
                anim.SetBool("IsAttacking", false);
            }
        }
    }
}