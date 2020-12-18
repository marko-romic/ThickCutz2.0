using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Animations;

public class ThirdPersonMovement : MonoBehaviour
{
    public float Speed = 1f;

    public Rigidbody rb;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Camera MainCam;
    private Animator Animator;
    public float SprintSpeed;

    
    private void Start()
    {
        Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

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
    }
    void FixedUpdate()
    {

        MovementHandler();

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
    void Attack()
    {
       
    }
}