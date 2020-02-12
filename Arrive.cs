using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive
{
    //Arrive-specific variables
    private float stopRadius = 5f;
    private float slowRadius = 10f;
    private float distanceToTarget;

    //Variables similar to Seek/Flee
    private Kinematic character;
    private GameObject target;
    private SteeringOutput currentDirection;

    private float idealSpeed = 0f;
    private float timeToTarget = 0.5f;
    private bool atTarget = false;

    public Arrive(Kinematic thisCharacter, ref GameObject target, ref SteeringOutput currentDirection, ref float maxSpeed, ref float maxAccel, ref float currentAccel, ref Vector3 linVelocity)
    {
        //Leftover from Seek()
        this.character = thisCharacter;
        this.target = target;

        currentDirection = this.getSteering();
        character.angularVelocity = 0;


        //Arrive Specific:
        distanceToTarget = currentDirection.linear.magnitude; //units of length 
        //Debug.Log("Distance to target: " + distanceToTarget);

        idealSpeed = determineSpeed(distanceToTarget, maxSpeed, maxAccel, linVelocity.magnitude); // idealSpeed returns how fast you need to go based on which circle you're in and how far you are from the target
                                                                                                  //Debug.Log("Ideal Speed: " + idealSpeed);



        currentAccel = (idealSpeed - character.linearVelocity.magnitude) / timeToTarget;  // needs to be units of length/time^2 - the difference between the speed you want to be going and the speed you are going (length/time) over time to slow down (length/time^2)
        //Debug.Log("Current Accel: " + currentAccel);

        if ((atTarget = true) && (idealSpeed == 0)) // trying to stop all velocity when it flags that you've hit your stop radius (aka you're 'atTarget')  -- This FOR SOME REASON removes any aspect of it slowing down, so I don't even know what programming does anymore
        {
            //currentAccel = 0.0f;
            //currentDirection.linear = Vector3.zero;
        }

    }

    public SteeringOutput getSteering() //this is the exact same as in seek
    {
        SteeringOutput seekDirection = new SteeringOutput();
        seekDirection.linear = target.transform.position - character.transform.position;

        return seekDirection;
    }


    public float determineSpeed(float distance, float maxSpeed, float maxAccel, float currentVelocity)
    {
        float speed = 0f;

        if (distance <= stopRadius)
        {
            atTarget = true; //Flag to stop the unit once it reaches the stop radius
            speed = 0f;
            //Debug.Log("AT TARGET:   TRUE");
        }
        else if ((distance > stopRadius) && (distance <= slowRadius)) //if between the slow and stop circles,
            speed = maxSpeed * (distance - stopRadius) / stopRadius; // eqtn given by Prof Slease
        else if (distance > slowRadius)
            speed = maxSpeed;

        return speed;
    }
}