using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI : MonoBehaviour
{
    // Debug configuration
    public GUIStyle myGUIStyle;
    public Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.DrawLine(rigidBody.position, rigidBody.position + movementDirection * 2.0f, Color.green, 0.0f, false);
        //Debug.DrawLine(rigidBody.position, rigidBody.position + currentFacingXZ * 2.0f, Color.red, 0.0f, false);
    }


    void OnGUI()
    {
        //GUI.Label(new Rect(750, 10, 100, 20), "Level " + level.ToString(), myGUIStyle);
        // debug text 
       // GUI.Label(new Rect(750, 10, 100, 20), "Angle " + angleDifferenceForward.ToString(), myGUIStyle);
        //GUI.Label(new Rect(20, 20, 280, 20), "Reach the top of the flag pole to beat the level! ", myGUIStyle);
       // GUI.Label(new Rect(20, 300, 360, 20), "Player has taken damage and has " + hp.ToString() + " left", myGUIStyle);
    }
}
 