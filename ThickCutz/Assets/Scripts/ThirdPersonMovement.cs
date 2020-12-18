using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public float Speed = 1f;

    public Rigidbody rb;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Camera MainCam;

    private void Start()
    {

        rb = GetComponent<Rigidbody>();

        rb.isKinematic = true;

    }
    void FixedUpdate()
    {

        MovementHandler();

    }
    void MovementHandler()
    {

        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        //values for movement and rotation from camera perspective
        Vector3 direction = MainCam.transform.right * hor + MainCam.transform.forward * ver; 
        direction.y = 0;

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