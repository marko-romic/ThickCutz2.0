using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public AIHelpers.MovementBehaviors activeMovementBehavior = AIHelpers.MovementBehaviors.None;
    public GameObject targetObject;
    public GameObject seekObject;
    public float maxSpeed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AIHelpers.InputParameters inputData = new AIHelpers.InputParameters(gameObject.transform, targetObject.transform, Time.deltaTime, maxSpeed);
        AIHelpers.MovementResult movementResult = new AIHelpers.MovementResult();

        switch (activeMovementBehavior)
        {
            case AIHelpers.MovementBehaviors.FleeKinematic:
                AIHelpers.FleeKinematic(inputData, ref movementResult);
                break;
            case AIHelpers.MovementBehaviors.SeekKinematic:
                AIHelpers.SeekKinematic(inputData, ref movementResult);
                break;
            case AIHelpers.MovementBehaviors.WanderKinematic:

                AIHelpers.WanderKinematic(inputData, ref movementResult);

                break;
            default:
                //AIHelpers.SeekKinematic(inputData, ref movementResult);
                movementResult.newPosition = transform.position;
                break;
        }

        gameObject.transform.position = movementResult.newPosition;
    }

    public void ActivateWander()
    {
        activeMovementBehavior = AIHelpers.MovementBehaviors.WanderKinematic;
    }

    public void ActivateSeek()
    {
        targetObject = seekObject;
        activeMovementBehavior = AIHelpers.MovementBehaviors.SeekKinematic;
    }

    public void ActivateLeave()
    {
        activeMovementBehavior = AIHelpers.MovementBehaviors.FleeKinematic;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
            Destroy(gameObject);
        }
    }

}
