using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Animations;

public class ThirdPersonMovement : MonoBehaviour
{
    public float Speed = 1f;
    public float attackTimer = 0.5f;
    public float attackTimerInAction;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    private float lastStateChange = 0.0f;
    public Rigidbody rb;
    //public Rigidbody eRb;

    public Camera MainCam;
    private Animator Animator;

    

    PlayerState currentState = PlayerState.onGround;

    public enum PlayerState
    {
        onGround,
        moveState,
        fallingDown,
        idle,
        attack,
        guard,
        parry,
        roll,
    }

    private void Start()
    {
        currentState = PlayerState.idle;

        Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        // eRb = GetComponent<Rigidbody>(); //rb of enemy for knockback

        rb.isKinematic = true;

    }

    void Update()
    {
        //SPAGHETTI :)
        /*if (Input.GetKey("w"))
        {
            Animator.SetBool("isRunning", true);
        }
        if (!Input.GetKey("w"))
        {
            Animator.SetBool("isRunning", false);
        }
        if (Input.GetKey("a"))
        {
            Animator.SetBool("isWalking", true);
        }
        if (!Input.GetKey("a"))
        {
            Animator.SetBool("isWalking", false);
        }
        if (Input.GetKey("d"))
        {
            Animator.SetBool("isWalking", true);
        }
        if (!Input.GetKey("d"))
        {
            Animator.SetBool("isWalking", false);
        }
        if (Input.GetKey("s"))
        {
            Animator.SetBool("isWalking", true);
        }
        if (!Input.GetKey("s"))
        {
            Animator.SetBool("isWalking", false);
        }*/

        //This is where the attackTimerInAction resets to the attackTimer so we can attack again
        if (Input.GetButtonDown("Attack"))
        {
            Animator.SetTrigger("Attack");
            attackTimerInAction = attackTimer;
            currentState = PlayerState.attack;
        }
        //guard bool
        if (Input.GetButtonDown("Guard"))
        {
            Animator.SetBool("Guard", true);
            currentState = PlayerState.guard;
        }
    }
    void FixedUpdate()
    {

        MovementHandler();
        StateMachine();
        

    }
    void MovementHandler()
    {
        //input mapping 
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        //values for movement and rotation from camera perspective
        Vector3 direction = MainCam.transform.right * hor + MainCam.transform.forward * ver;
        direction.y = 0;

        //the float in the animator is being set by the magnitude(total value of vector) of the direction
        Animator.SetFloat("Speed", direction.magnitude);

        if (direction.magnitude >= 0.1f)
        {
            //player turning based off of angles targeted
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            //turning the player
            rb.rotation = Quaternion.Euler(0f, angle, 0f);

            //THIS is moving the character
            rb.MovePosition(rb.position + direction * Speed * Time.fixedDeltaTime);
            
        }
    }
    public void StateMachine()
    {
        switch (currentState)
        {
            case PlayerState.idle:

                return;

            case PlayerState.attack:
                AttackState();
                return;

            case PlayerState.roll:
                //insert roll function
                return;

            case PlayerState.guard:
                GuardState();
                return;

            case PlayerState.parry:
                //insert parry handler
                return;

        }
        void AttackState()
        {
            attackTimerInAction -= Time.deltaTime; //deincrementing

            if (attackTimerInAction == 0f) // seeing if attack timer is equal to 0
            {
                currentState = PlayerState.idle; //bring state back to idle
            }
        }
        void GuardState()
        {
            if (Input.GetButtonUp("Guard")) // Bug where guard sometimes switches off back to idle. However 6/10 times it will stay in guard unless a trigger is hit
            {
                Animator.SetBool("Guard", false); //setting bool to false
                currentState = PlayerState.idle;//setting the state back to idle when false
            }
        }
    }

    //Knockback
        //public float force = 500; // adjust the impact force

    /*void OnTriggerEnter(Collider other)
    {
        Vector3 dir = other.transform.position - transform.position;
        dir.y = 0; // keep the force horizontal
                   
        if (other.gameObject.tag == "Enemy")
        { // use AddForce for rigidbodies:
            rb.AddForce(-transform.forward * force);
        }
    }*/
}

