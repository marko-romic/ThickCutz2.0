using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
//using UnityEngine.Assertions.Must;

public class Roll : MonoBehaviour
{
    //public GameObject StartObj;
    //public GameObject RollTargetRight;
    //public GameObject RollTargetLeft;
    //public float LERPDuration = 0;
    //public float rollTimer = 0.5f;

    private MovementState currentState;
    //private bool RollingRight;
    //private bool RollingLeft;

    public float RollTimer = 0.5f;
    public float RollingTimer;

    public Rigidbody rb;
    public float RollSpeed = 1f;

    public enum MovementState
    {
        rollingRight,
        rollingLeft,
        neutral
    }
    //private Vector3 rightRoll;
    //private Vector3 leftRoll;
    //private Vector3 target;
    //private Vector3 startPos;
    //private Vector3 endPos;
    //private float accumatedTimeSinceLERPStart;

    // Start is called before the first frame update
    void Start()
    {
        currentState = MovementState.neutral;
        rb = GetComponent<Rigidbody>();
        /*if (StartObj != null)
        {
            startPos = StartObj.transform.position;

        }*/

        /*if (RollTargetRight != null)
        {
            endPos = RollTargetRight.transform.position;
        }

        accumatedTimeSinceLERPStart = 0.0f;*/
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

        //if (currentState != MovementState.neutral)
        //{
            //RollHandler();

        //}
        /*else if(accumatedTimeSinceLERPStart > 0.0f)
        {
            accumatedTimeSinceLERPStart = 0.0f;
        }*/
    }

    void StateMachine()
    {
        //Vector3 RollVector = new Vector3();
        switch (currentState)
        {
            case MovementState.rollingRight:
                RollingRightHandler();
                /*rightRoll = RollTargetRight.transform.position;
                RollVector = transform.right * RollSpeed * Time.fixedDeltaTime;*/
                return;

            case MovementState.rollingLeft:
                RollingLeftHandler();
                /*endPos = RollTargetLeft.transform.position;
                RollVector = -transform.right * RollSpeed * Time.fixedDeltaTime;*/
                return;

            case MovementState.neutral:
                return;
        }

        /*if (transform.position == endPos)
        {
            currentState = MovementState.neutral;
        }
        else
        {
            accumatedTimeSinceLERPStart += Time.deltaTime;
            Vector3 TargetPosition = rb.position; // + RollVector;
            TargetPosition.y = 0f;
            rb.MovePosition(TargetPosition); */

    }
    void RollingRightHandler()
    {
        RollingTimer -= Time.deltaTime; //deincrementing
        rb.MovePosition(transform.right * RollSpeed * Time.deltaTime);

        if (RollingTimer == 0f) // seeing if rolling timer is equal to 0
        {
            currentState = MovementState.neutral; //bring state back to idle
        }
    }
    void RollingLeftHandler()
    {
        RollingTimer -= Time.deltaTime; //deincrementing
        rb.MovePosition(-transform.right * RollSpeed * Time.deltaTime);

        if (RollingTimer == 0f) // seeing if rolling timer is equal to 0
        {
            currentState = MovementState.neutral; //bring state back to idle
        }
    } 
}

        
        /*if (accumatedTimeSinceLERPStart < LERPDuration)
        {
            // Step 1, pass the accumulated time to TimeFunction to get the correct T value
            float t = EasingFunctions.TimeFunction(accumatedTimeSinceLERPStart, LERPDuration);

            // Step 2, Use that T value to LERP and get the new position
            Vector3 newPosition = Vector3.Lerp(startPos, endPos, t);

            // Step 3, ovveride the existing position with the new position from the LERP
            transform.position = newPosition;

        }
        
    }
}*/
