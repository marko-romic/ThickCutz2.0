using UnityEngine;
using UnityEditor;
using System;

public class AIHelpers
{
    static private float maxWanderDuration = 2.0f;
    static private float wanderCounter = 0.0f;
    static private System.Random r = new System.Random();

    public class MovementResult
    {
        public Vector3 newPosition = Vector3.zero;
        public Vector3 newOrientation = Vector3.zero;
    }

    public class InputParameters
    {
        public InputParameters(Transform current, Transform target, float updateDelta, float speed)
        {
            currentTransform = current;
            targetTransform = target;
            currentUpdateDuration = updateDelta;
            maxSpeed = speed;
        }

        public InputParameters(InputParameters o)
        {
            currentTransform = o.currentTransform;
            targetTransform = o.targetTransform;
            currentUpdateDuration = o.currentUpdateDuration;
            maxSpeed = o.maxSpeed;
        }

        public InputParameters()
        {
            currentUpdateDuration = 0.0f;
            maxSpeed = 1.0f;
        }

        public Transform currentTransform;
        public Transform targetTransform;
        public float currentUpdateDuration;
        public float maxSpeed;
    }

    public enum MovementBehaviors
    {
        None,
        SeekKinematic,
        FleeKinematic,
        WanderKinematic
    }

    internal static void SeekKinematic(InputParameters inputData, ref MovementResult result)
    {
        Vector3 directionToTarget = inputData.targetTransform.position - inputData.currentTransform.position;
        directionToTarget.Normalize();

        result.newPosition = inputData.currentTransform.position + (directionToTarget * inputData.maxSpeed * inputData.currentUpdateDuration);
    }

    internal static void FleeKinematic(InputParameters inputData, ref MovementResult result)
    {
        Vector3 directionToTarget = inputData.currentTransform.position - inputData.targetTransform.position;
        directionToTarget.Normalize();

        result.newPosition = inputData.currentTransform.position + (directionToTarget * inputData.maxSpeed * inputData.currentUpdateDuration);
    }

    internal static void WanderKinematic(InputParameters inputData, ref MovementResult result)
    {
        int range = 5;
        
        wanderCounter += inputData.currentUpdateDuration;
        if (wanderCounter > maxWanderDuration)
        {
            Vector3 randomTarget = inputData.targetTransform.position;
            randomTarget.x += (float)r.NextDouble() * range;
            randomTarget.z += (float)r.NextDouble() * range;
            inputData.targetTransform.position = randomTarget;
            wanderCounter = 0.0f;
        }

        SeekKinematic(inputData, ref result);
    }
}