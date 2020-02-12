using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kinematic : MonoBehaviour
{
    public Vector3 linearVelocity;
    public float angularVelocity;

    public float maxLinearVelocity = 10f;
    public float maxAngularVelocity = 23f;
    public float maxAccel = 2.3f;
    private float currentAccel = 0f;

    public GameObject myTarget;
    private SteeringOutput currentDirection;


    //Check boxes for easily choosing which behaviors to apply:
    public bool seeking = false;
    public bool fleeing = false;
    public bool arriving = false;
    public bool aligning = false;
    public bool facing = false;
    public bool LookWhereYoureGoing = false;

    private void Start()
    {
        currentDirection = new SteeringOutput();

    }


    // Update is called once per frame
    void Update()
    {



        if (seeking)
        {
            Seek mySeek = new Seek(this, ref myTarget, ref currentDirection, ref currentAccel, ref maxAccel, 1);
        }

        if (fleeing) //Combined Seek & Flee into one script
        {
            Seek mySeek = new Seek(this, ref myTarget, ref currentDirection, ref currentAccel, ref maxAccel, 2);
        }

        if (arriving) //NOTE: only check Arrive to execute; Seek is not required
        {
            Arrive myArrive = new Arrive(this, ref myTarget, ref currentDirection, ref maxLinearVelocity, ref maxAccel, ref currentAccel, ref linearVelocity);
        }

        //Debug.Log("Current Direction: " + currentDirection.linear);
        //Debug.Log("Current Accel: " + currentAccel);

        updateVelocities();
        updateMovement();

    }



    void updateVelocities()
    {
        //set the new vector to be in the direction we want at the max acceleration possible
        currentDirection.linear.Normalize();
        currentDirection.linear *= currentAccel;


        // update linear and angular velocities
        if (currentDirection != null)
        {
            linearVelocity += currentDirection.linear * Time.deltaTime;
            angularVelocity += currentDirection.angular * Time.deltaTime;
        }
        else
        {
            linearVelocity = Vector3.zero;
        }


        //Speed Limiter (Wee-Ooh it's the cops)
        if (linearVelocity.magnitude > maxLinearVelocity)
        {
            linearVelocity.y = maxLinearVelocity;
        }

        if (angularVelocity > maxAngularVelocity)
        {
            angularVelocity = maxAngularVelocity;
        }

        return;
    }

    void updateMovement()
    {
        // update my position and rotation
        this.transform.position += linearVelocity * Time.deltaTime;

        Vector3 v = new Vector3(0, angularVelocity, 0);
        this.transform.eulerAngles += v * Time.deltaTime;

        return;
    }

}