using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek
{
    private Kinematic character;
    private GameObject target;
    private SteeringOutput currentDirection;
    //private float currentAcceleration = 0f;
    //private float maxAcceleration = 0f;

    public Seek(Kinematic thisCharacter, ref GameObject target, ref SteeringOutput currentDirection, ref float currentAccel, ref float maxAccel, int choice)
    {
        this.character = thisCharacter;
        this.target = target;
        currentAccel = maxAccel;
        currentDirection = this.getSteering(choice);
        thisCharacter.angularVelocity = 0f;

    }

    public SteeringOutput getSteering(int choice)
    {

        SteeringOutput seekDirection = new SteeringOutput();


        if (choice == 1) // Seek only
            seekDirection.linear = target.transform.position - character.transform.position;
        else if (choice == 2) // Flee only
            seekDirection.linear = character.transform.position - target.transform.position;


        return seekDirection;
    }
}