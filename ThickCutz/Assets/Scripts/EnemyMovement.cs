using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public Transform player;
    public Animator anim;
    public Rigidbody eRb;
    private Vector3 moveDirection;
    public float Speed = 1f;

    public enum EnemyState
    {
        Idle,
        Walking,
        Running,
        Patrol,
        Attack,
    }

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        eRb = GetComponent<Rigidbody>();

        eRb.isKinematic = true;
    }

    private void FixedUpdate()
    {
        eRb.MovePosition(eRb.position + moveDirection * Speed * Time.fixedDeltaTime); //movement to the player
    }

    void Update()
    {
        moveDirection = Vector3.zero; //set to 0 at the start

        if (Vector3.Distance(player.position, this.transform.position) < 10) //distance from player to move
        {

            if (Vector3.Distance(player.position, this.transform.position) < 10)
            {
                Vector3 direction = player.position - this.transform.position;
                direction.y = 0;

                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f); //looking from a spherical location to find the player

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
                anim.SetBool("IsAttacking", false); //works but isnt CLEAN 
            }
        }
    }
    public void StateMachine()
    {
        //got the if else working instead of statemachine. time crunch so moving on
    }

    public void OnTriggerEnter(Collision other) //attempted to add knockback on hit with player sword
    {
        if (other.gameObject.tag == "Sword")
        {
            eRb.AddForce(-transform.forward * 1);
        }
    }
}