using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    public Vector3 force;
    public Vector3 velocity;
    public float maxVelocity = 100;

    private SteeringBehaviour[] behaviours;

    // Use this for initialization
    void Start()
    {
        behaviours = GetComponents<SteeringBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        ComputeForces();
        ApplyVelocity();
    }

    void ComputeForces()
    {
        //SET force to Zero
        force = Vector3.zero; // When you create a variable in a function,
                                      // It only exists within that function
        //FOR i = 0 to behaviours count
        
        for(int i = 0; i < behaviours.Length; i++)
        {
            SteeringBehaviour behaviour = behaviours[i];
            //IF behaiour is not enabled
            if (!behaviour.enabled)
            {
                //CONTINUE
                continue;
            }
            //SET force to force + bahaviour's force
            force += behaviour.GetForce();
            //IF force is greater thean maxVelocity
            if (force.magnitude > maxVelocity)
            {
                //SET force to force normlized x maxVelocity
                force = force.normalized * maxVelocity;
                //BREAK
                break;
            }
        }
    }

    void ApplyVelocity()
    {
        // SET velocity to velocity + force x delta time
        velocity += force * Time.deltaTime;
        // IF velocity is greater than maxVelocity
        if (velocity.magnitude > maxVelocity)
        {
            // SET velocity to velocity normlized x maxVelcity
            velocity = velocity.normalized * maxVelocity;
        }
        //IF velocity is greater than zero
        if(velocity != Vector3.zero)
        {
            //SET postion to postion + velocity x delta time
            transform.position += velocity * Time.deltaTime;
            //SET rotation to Quaternion.lookRotation velocity
            transform.rotation = Quaternion.LookRotation(velocity);
        }
    }

}
