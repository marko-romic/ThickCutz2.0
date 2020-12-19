using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling : MonoBehaviour
{
    private MovementState currentState;

    public float RollTimer = 1.0f;
    public float RollingTimer;

    public Rigidbody rb;
    public float RollSpeed = 5f;

    public enum MovementState
    {
        rollingRight,
        rollingLeft,
        neutral
    }

    void Start()
    {
        currentState = MovementState.neutral;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void FixedUpdate()
    {
        StateMachine();
    }
    void Update()
    {
        if (Input.GetKey("e"))
        {

            RollingTimer = RollTimer;
            currentState = MovementState.rollingRight;
        }

        if (Input.GetKey("q"))
        {
            RollingTimer = RollTimer;
            currentState = MovementState.rollingLeft;
        }
    }
    void StateMachine()
    {
        switch (currentState)
        {
            case MovementState.rollingRight:
                RollingRightHandler();

                return;

            case MovementState.rollingLeft:
                RollingLeftHandler();

                return;

            case MovementState.neutral:
                return;
        }
    }
    void RollingRightHandler()
    {
        Vector3 moveDirectionRight = rb.transform.right;
        moveDirectionRight.y = 0;

        RollingTimer -= 0.5f; //deincrementing
        rb.MovePosition(rb.position + moveDirectionRight * RollSpeed * Time.deltaTime);

        if (RollingTimer == 0f) // seeing if rolling timer is equal to 0
        {
            currentState = MovementState.neutral; //bring state back to idle
        }
    }
    void RollingLeftHandler()
    {
        Vector3 moveDirectionLeft = -rb.transform.right;
        moveDirectionLeft.y = 0;

        RollingTimer -= 0.5f; //deincrementing
        rb.MovePosition(rb.position + moveDirectionLeft * RollSpeed * Time.deltaTime);

        if (RollingTimer == 0f) // seeing if rolling timer is equal to 0
        {
            currentState = MovementState.neutral; //bring state back to idle
        }
    }
}
