using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class roll : MonoBehaviour
{
    public GameObject StartObj;
    public GameObject RollTargetRight;
    public GameObject RollTargetLeft;
    public float LERPDuration = 0;
    public MovementState currentState;

    public Rigidbody rb;
    public float RollSpeed;

    public enum MovementState{
        rollingRight,
        rollingLeft,
        neutral
    }

    //private Vector3 target;
    private Vector3 startPos;
    private Vector3 endPos;
    private float accumatedTimeSinceLERPStart;

    // Start is called before the first frame update
    void Start()
    {
        if (StartObj != null)
        {
            startPos = StartObj.transform.position;

        }

        if (RollTargetRight != null)
        {
            endPos = RollTargetRight.transform.position;
        }

        accumatedTimeSinceLERPStart = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("e"))
        {
            currentState = MovementState.rollingRight;
            
        }
        if (Input.GetKey("q"))
        {
            currentState = MovementState.rollingLeft;

        }

        if (currentState != MovementState.neutral)
        {
            RollHandler();

        }
        else if(accumatedTimeSinceLERPStart > 0.0f)
        {
            accumatedTimeSinceLERPStart = 0.0f;
        }
    }

    void RollHandler()
    {
        Vector3 RollVector = new Vector3();
        switch (currentState)
        {
            case MovementState.rollingRight:
                endPos = RollTargetRight.transform.position;
                RollVector = transform.right * RollSpeed * Time.fixedDeltaTime;
                break;

            case MovementState.rollingLeft:
                endPos = RollTargetLeft.transform.position;
                RollVector = -transform.right * RollSpeed * Time.fixedDeltaTime;
                break;
            
        }

        if (transform.position == endPos)
        {
            currentState = MovementState.neutral;
        }
        else
        {
            accumatedTimeSinceLERPStart += Time.deltaTime;
            Vector3 TargetPosition = rb.position; // + RollVector;
            rb.MovePosition(TargetPosition); 

        }
        

        
        if (accumatedTimeSinceLERPStart < LERPDuration)
        {
            // Step 1, pass the accumulated time to TimeFunction to get the correct T value
            float t = EasingFunctions.TimeFunction(accumatedTimeSinceLERPStart, LERPDuration);

            // Step 2, Use that T value to LERP and get the new position
            Vector3 newPosition = Vector3.Lerp(startPos, endPos, t);

            // Step 3, ovveride the existing position with the new position from the LERP
            transform.position = newPosition;

        }
        
    }
}
