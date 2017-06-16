using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIAgent))]
public class SteeringBehaviour : MonoBehaviour
{
    public float weighting = 7.5f;
    [HideInInspector] public AIAgent owner;


    // Use this for initialization
    void Awake()
    {
        owner = GetComponent<AIAgent>();

    }

    //virtual funtion that will be in charge of calulating
    // A force depending on the behaviour that is overriding it
    public virtual Vector3 GetForce()
    {
        return Vector3.zero;
    }
}
