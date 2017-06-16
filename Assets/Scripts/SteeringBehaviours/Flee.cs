using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : SteeringBehaviour
{
    public Transform target;
    public float stoppingDistance = 5f;
    //private Vector3 desiredForce;

    public override Vector3 GetForce()
    {
        // SET force to zero
        Vector3 force = Vector3.zero;

        // IF target is null
        if (target == null)
        {
            //RETURN force
            return force;
        }
        // Direction = target - owner's position
        // SET desiredForce to direction from target to owner's position
        Vector3 desiredForce = target.position - owner.transform.position; //Changed from - to +
        // SET desiredForce y to zero
        desiredForce.y = 0f;
        // IF desiredForce.magnitude is greater than stoppingDistance
        if (desiredForce.magnitude > stoppingDistance)
        {
            // SET desiredForce to desiredForce normalized x weighting
            desiredForce = desiredForce.normalized * weighting;
            // SET force to desiredForce - owner's velcity
            force = desiredForce - owner.velocity; //Changed from - to +
        }
        // RETURN force
        return force;
    }
}
