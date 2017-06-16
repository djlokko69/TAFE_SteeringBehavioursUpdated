using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGL;

public class Wander : SteeringBehaviour
{
    public float offset = 1.0f;
    public float radius = 1.0f;
    public float jitter = 0.2f;

    private Vector3 targetDir;
    private Vector3 randomDir;
    private Vector3 circlePos;

    public override Vector3 GetForce()
    {
        // Set force to zero
        Vector3 force = Vector3.zero;

        // Generating a random number between
        // - half max value to half max value
        /*
        -32,767    16,363     0     16,383      32,767
        |---------------------|-------------------|
                   |_________________|
                       Random Range
        */
        // 0x7fff = 32767
        float randX = Random.Range(0, 0x7fff) - (0x7fff * 0.5f);
        float randZ = Random.Range(0, 0x7fff) - (0x7fff * 0.5f);

        #region Calculate RandomDir
        // SET randomDir to new vector3 x = randX & z = randZ
        randomDir = new Vector3(randX, 0, randZ);
        // SET randomDir to normalized randomDir
        randomDir = randomDir.normalized;
        // SET randomDir to randomDir x jitter
        randomDir *= jitter;

        /* examples
        // SET maxVelocity to maxVelocity x velocity;
        maxVelocity *= velocity;
        
        // SET maxVelocity to maxVelocity + velocity;
        maxVelocity += velocity;
        
        // SET maxVelocity to maxVelocity - velocity;
        maxVelocity -= velocity;

        >     greater than              5 > 4 is TRUE
        <     less than                 4 < 5 is TRUE
        >=    greater than or equal     4 >= 4 is TRUE
        <=    less than or equal        3 <= 4 is TRUE
        ==    equal to                  5 == 5 is TRUE
        !=    not equal to              5 != 4 is TRUE

        */
        #endregion

        #region Calculate TargetDir
        // SET targetDir to targetDir + randomDir
        targetDir += randomDir;
        // SET targetDir to normalized targetDir
        targetDir = targetDir.normalized;
        // SET targetDir to targetDir x raduis
        targetDir *= radius;
        #endregion

        #region Calculate force
        // SET seekPos to owner's postition + targetDir
        Vector3 seekPos = owner.transform.position + targetDir;// not the answer
        // SET seekPos to seekPos + owner's forward x offset
        seekPos += owner.transform.forward * offset;

        #region Gizmos
        Vector3 offsetPos = transform.position + transform.forward.normalized * offset;
        GizmosGL.AddCircle(offsetPos + Vector3.up * 0.1f, radius, Quaternion.LookRotation(Vector3.down), 16, Color.red);
        
        GizmosGL.AddCircle(seekPos + Vector3.up * 0.15f, radius * 0.6f, Quaternion.LookRotation(Vector3.down), 16, Color.blue);
        #endregion
        // SET desiredForce to seekPos - position
        Vector3 desiredForce = seekPos - transform.position;
        // IF desiredForce is not zero
        if (desiredForce != Vector3.zero)
        {
            // SET desiredForce to desiredForce normalized x weighting
            desiredForce = desiredForce.normalized * weighting;
            // SET force to desiredForce - owner's velocity
            force = desiredForce - owner.velocity;
        }
        #endregion

        // Return force
        return force;
    }
}
