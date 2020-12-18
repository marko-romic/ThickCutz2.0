using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KinematicInput : MonoBehaviour
{

    // Jump and Fall parameters
    public float maxJumpSpeed = 1.5f;
    public float maxFallSpeed = -2.2f;
    public float timeToMaxJumpSpeed = 0.2f;
    public float deccelerationDuration = 0.0f;
    public float maxJumpDuration = 1.2f;

    //Roll left and right parameters
    public float forceMult = 200;
    public float xPos;
    public UnityEngine.Vector3 forceVector;
    //Rigidbody rb;
    public float decceleration = 15f;
    public float timeElapsed;
    public float maxTime;
    public float chargeRate;
    public float maxPower;
    float zPos_0;
    public float force = 500f;
    public bool push;
    public Transform cube;
    public Transform cubeie;
    public GameObject theSlider;
    public GameObject spawn;
    public Text score;

    // Horizontal movement parameters
    Vector3 movementVector;
    Vector3 horizontalVector;
    public float speed = 10.0f;

    public Camera MainCamera;

    public float eulerPitch = 0.0f;
    public float eulerYaw = -180.0f;
    public Quaternion CurrentRotation;

    float currentFOVChangeDuration = 0.0f;
    Vector3 _cameraOffset;
    Vector3 CurrentOrientationFromPlayer;

    enum PlayerState
    {
        onGround,
        walking,
        fallingDown,
        attacking,
        rolling,
        guard,
        parry,
        slice,
    }

    PlayerState currentState = PlayerState.onGround;

    bool rolling = false;
    bool jumpStartRequest = false;
    bool jumpRelease = false;
    bool isMovingUp = false;
    bool isFalling = false;
    float currentJumpDuration = 0.0f;
    float currentFallDuration = 0.0f;
    float gravityAcceleration = -9.8f;

    public float groundSearchLength = 0.6f;
    RaycastHit currentGroundHit;

    // Rotation Parameters
    float angleDifferenceForward = 0.0f;

    // Components and helpers
    public Rigidbody rigidBody;
    Vector2 input;
    Vector3 playerSize;

    // Debug configuration
    public GUIStyle myGUIStyle;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        playerSize = GetComponent<Collider>().bounds.size;
    }

    void Start()
    {
        /*jumpStartRequest = false;
        jumpRelease = false;
        isMovingUp = false;
        isFalling = false;*/
        currentState = PlayerState.fallingDown;
        CurrentRotation.eulerAngles += new Vector3(eulerPitch, eulerYaw, 0.0f);
        CurrentOrientationFromPlayer = CurrentRotation * Vector3.forward;
        push = false;
        theSlider.GetComponent<Slider>().maxValue = maxPower;
        score.GetComponent<Text>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        input = new Vector2();
        input.x = horizontal;
        input.y = vertical;

        if (Input.GetButtonDown("Jump"))
        {
            jumpStartRequest = true;
            jumpRelease = false;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            jumpRelease = true;
            jumpStartRequest = false;
        }

        //Roll Right
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Rolling Right!");
            zPos_0 = rigidBody.transform.position.x;
            if (maxTime > timeElapsed)
            {
                timeElapsed += Time.fixedDeltaTime;
                forceVector.z = force * timeElapsed + (0.5f * decceleration * (timeElapsed * timeElapsed));
            }
            rigidBody.MovePosition((forceVector + transform.position) * Time.fixedDeltaTime);
        }

        if (Input.GetKeyDown("space"))
        {
            push = true;
            timeElapsed = 0f;
            maxTime = -force / decceleration;
        }
        if (!push)
        //charging slider
        {
            force = Mathf.Abs(Mathf.Sin(Time.time * chargeRate) * maxPower);
        }
        theSlider.GetComponent<Slider>().value = force;

        Score();

        if (Input.GetKeyDown("z"))
        {
            rigidBody.transform.position = spawn.transform.position;
            force = 0f;
            forceVector = UnityEngine.Vector3.zero;
            push = false;
        }
        // Vector3 targetPosition = rigidBody.position + movementVector;
        //xPos = rigidBody.transform.position.z;
    }

      //  //Roll Left
      //  if (Input.GetKeyDown(KeyCode.Q))
      //  {
       //     Debug.Log("Rolling Left!");
        //    Vector3 newPosition = transform.position + (transform.forward * Time.deltaTime);
        //    rigidBody.MovePosition(newPosition);
      //  }

        //if (KeyCode(Keypress))
  //  }

    //void StartFalling()
    //{
        //isMovingUp = false;
        //isFalling = true;
       // currentState = PlayerState.fallingDown;
      //  currentJumpDuration = 0.0f;
       // currentFallDuration = 0.0f;
       // jumpRelease = false;
    //}

    void FixedUpdate()
    {
        //AMAZING CAMERA ATTEMPTS

        Vector3 Movement = MainCamera.transform.right * input.x * speed * Time.fixedDeltaTime;
        //Vector3 Movement = Quaternion.MainCamera.RotateTowards(transform.rotation) * input.x * speed * Time.deltaTime;
        //Vector3 Movement = MainCamera.transform.Rotate(Vector3, Time.deltaTime, Space.World);
        //Vector3 Movement = MainCamera.transform.Rotate(rigidBody.position.x, transform.position.y, Time.deltaTime) //Mathf.Lerp(transform.position, t, input.x * Time.deltaTime));
        //Movement += MainCamera.transform.forward * input.y * speed * Time.fixedDeltaTime;

        //Attempt to rotate player with camera

        //Vector3 Rotation = rb.transform.position * input.x * speed * Time.fixedDeltaTime;
        //Rotation += rb.transform.forward * input.y * speed * Time.fixedDeltaTime;

        //Previous Camera script 

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.rotation.x, MainCamera.transform.rotation.eulerAngles.y,transform.rotation.z), MainCamera.GetComponent<CameraControls>().rotationSpeed);
        //transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
        //DISCO DISCO (Vector3.up, MainCamera.transform.rotation.y)
        //transform.Rotate(Vector3.up, MainCamera.transform.rotation.eulerAngles.y);
        #region commentOut
        // Calculate horizontal movement
        // Vector3 movement = Vector3.right * input.x * speed * Time.deltaTime;
        // movement += Vector3.forward * input.y * speed * Time.deltaTime;
        // movement.y = 0.0f;
        //Vector3 targetPosition = rigidBody.position + movement;

        // Calculate Vertical movement
        // float targetHeight = 0.0f;

        //if (currentState != PlayerState.jumpingUp && jumpStartRequest && isOnGround())
        //{
        //  isMovingUp = true;
        //   currentState = PlayerState.jumpingUp;
        //   jumpStartRequest = false;
        //   currentJumpDuration = 0.0f;
        //}

        // if (currentState == PlayerState.jumpingUp)
        //{
        //   if (jumpRelease || currentJumpDuration >= maxJumpDuration)
        //    {
        //        StartFalling();
        //   }
        //   else
        //   {
        //       float currentYpos = rigidBody.position.y;
        //      float newVerticalVelocity = maxJumpSpeed + gravityAcceleration * Time.deltaTime;
        //      targetHeight = currentYpos + (newVerticalVelocity * Time.deltaTime) + (0.5f * maxJumpSpeed * Time.deltaTime * Time.deltaTime);
        //
        //      currentJumpDuration += Time.deltaTime;
        //  }
        // }
        // else if (!isOnGround())
        //{
        //     StartFalling();
        // }
        // if (currentState == PlayerState.fallingDown)
        //  {
        //     currentFallDuration += Time.fixedDeltaTime;
        //  if (isOnGround())
        //  {
        // End of falling state. No more height adjustments required, just snap to the new ground position
        //isFalling = false;

        //    currentState = PlayerState.onGround;
        //     targetHeight = currentGroundHit.point.y + (0.5f * playerSize.y);
        //  }
        //   else if (jumpStartRequest)
        //  {
        //      currentState = PlayerState.jumpingUp;
        //  }
        //  {
        //    float currentYpos = rigidBody.position.y;
        //     float currentYvelocity = rigidBody.velocity.y;

        //    float newVerticalVelocity = maxFallSpeed + gravityAcceleration * Time.deltaTime;
        //    targetHeight = currentYpos + (newVerticalVelocity * Time.deltaTime) + (0.5f * maxFallSpeed * Time.deltaTime * Time.deltaTime);
        // }
        // }

        // if (targetHeight > Mathf.Epsilon)
        // {
        // Only required if we actually need to adjust height
        //     targetPosition.y = targetHeight;
        // }

        // Calculate new desired rotation
        //Vector3 movementDirection = targetPosition - rigidBody.position;
        // movementDirection.y = 0.0f;
        //movementDirection.Normalize();

        // Vector3 currentFacingXZ = transform.forward;
        // currentFacingXZ.y = 0.0f;

        // angleDifferenceForward = Vector3.SignedAngle(movementDirection, currentFacingXZ, Vector3.up);
        //Vector3 targetAngularVelocity = Vector3.zero;
        //targetAngularVelocity.y = angleDifferenceForward * Mathf.Deg2Rad;

        // Quaternion syncRotation = Quaternion.identity;
        // syncRotation = Quaternion.LookRotation(movementDirection);

        // Debug.DrawLine(rigidBody.position, rigidBody.position + movementDirection * 2.0f, Color.green, 0.0f, false);
        // Debug.DrawLine(rigidBody.position, rigidBody.position + currentFacingXZ * 2.0f, Color.red, 0.0f, false);

        // Finally, update RigidBody    
        // rigidBody.MovePosition(targetPosition);

        // if (movement.sqrMagnitude > Mathf.Epsilon)
        // {
        // Currently we only update the facing of the character if there's been any movement
        //     rigidBody.MoveRotation(syncRotation);
        // }
        #endregion
        horizontalVector.x = Input.GetAxis("Horizontal");
        horizontalVector.z = Input.GetAxis("Vertical");

        MovementHandler();
        
        StateMachine();
    }

    void MovementHandler()
    {
        movementVector = transform.right * horizontalVector.x * speed * Time.fixedDeltaTime;
        movementVector += transform.forward * horizontalVector.z * speed * Time.fixedDeltaTime;

        Vector3 targetPosition = rigidBody.position + movementVector;

        rigidBody.MovePosition(targetPosition);

        

        if (Input.GetKeyUp(KeyCode.E))
        {

        }
    }

    //private bool isOnGround()
   // {
     //   Vector3 lineStart = transform.position;
      //  Vector3 vectorToSearch = new Vector3(lineStart.x, lineStart.y - groundSearchLength, lineStart.z);

      //  Debug.DrawLine(lineStart, vectorToSearch);

     //   return;  //Physics.Linecast(lineStart, vectorToSearch, out currentGroundHit);
    //}
    
    void StateMachine()
    {
        
    }

    void OnGUI()
    {
        // Add here any debug text that might be helpful for you
        GUI.Label(new Rect(10, 10, 100, 20), "Angle " + angleDifferenceForward.ToString(), myGUIStyle);

        if (rolling == true)
        {
            GUI.Label(new Rect(40, 10, 100, 20), "Rolling" + angleDifferenceForward.ToString(), myGUIStyle);
        }
    }

    private void OnDrawGizmos()
    {
        // Debug Draw last ground collision, helps visualize errors when landing from a jump
        if (currentGroundHit.collider != null)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(currentGroundHit.point, 0.25f);
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        // Debug-draw all contact points and normals, helps visualize collisions when the physics of the RigidBody are enabled (when is NOT Kinematic)
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            SceneManager.LoadScene("WinScene");
        }
    }

    void Score()
    {
        if (cube)
        {
            float dist = UnityEngine.Vector3.Distance(cube.position, cubeie.position);
            score.text = ("Score is: ") + dist;
        }
    }
}
