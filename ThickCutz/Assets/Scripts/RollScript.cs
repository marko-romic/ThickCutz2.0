﻿using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class RollScript : MonoBehaviour
{ 
    Rigidbody rb;
    public float decceleration = 15f;
    public float timeElapsed;
    public float maxTime;
    public float chargeRate;
    public float maxPower;
    float zPos_0;
    public float force = 500f;
    public bool rollRight;
    public bool rollLeft;
    public Transform cube;
    public Transform cubeie;
    public Text score;
    public GameObject theSlider;
    public GameObject spawn;
    public UnityEngine.Vector3 forceVector;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rollRight = false;
        rollLeft = false;
        theSlider.GetComponent<Slider>().maxValue = maxPower;
        score.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            rollRight = true;
            timeElapsed = 0f;
            maxTime = -force / decceleration;
        }
        if (Input.GetKeyDown("q"))
        {
            rollLeft = true;
            timeElapsed = 0f;
            maxTime = -force / decceleration;
        }
        if (!rollRight)
            //charging slider
        {
            force = Mathf.Abs(Mathf.Sin(Time.time * chargeRate) * maxPower);
        }
        theSlider.GetComponent<Slider>().value = force;
        if (!rollLeft)
        //charging slider
        {
            force = Mathf.Abs(Mathf.Sin(Time.time * chargeRate) * maxPower);
        }
        theSlider.GetComponent<Slider>().value = force;

        Score();

        if (Input.GetKeyDown("z"))
        {
            rb.transform.position = spawn.transform.position;
            force = 0f;
            forceVector = UnityEngine.Vector3.zero;
            rollRight = false;
            rollLeft = false;
        }
    }
    private void FixedUpdate()
    {
        if (rollRight)
        {
            zPos_0 = rb.transform.position.x;
            if (maxTime > timeElapsed)
            {
                timeElapsed += Time.fixedDeltaTime;
                forceVector.x = force * timeElapsed + (0.5f * decceleration * (timeElapsed * timeElapsed));  
            }
            rb.MovePosition((forceVector + transform.position) * Time.fixedDeltaTime);
        }
        if (rollLeft)
        {
            zPos_0 -= rb.transform.position.x;
            if (maxTime > timeElapsed)
            {
                timeElapsed += Time.fixedDeltaTime;
                forceVector.x = force * timeElapsed + (0.5f * decceleration * (timeElapsed * timeElapsed));
            }
            rb.MovePosition((forceVector + transform.position) * Time.fixedDeltaTime);
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