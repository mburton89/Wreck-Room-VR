using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresencePhysics : MonoBehaviour
{
    public Transform target;
    private Rigidbody rb;
    public Renderer nonPhysicalHand;
    public float showNonPhysicalHandDistance = 0.05f;

    //Fist Attack Variables
    public GameObject handPresence;
    bool isClenched = false;
    private float timeClenched = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        /*float distance = Vector3.Distance(transform.position, target.position);

        if(distance < showNonPhysicalHandDistance)
        {
            nonPhysicalHand.enabled = true;
        }
        else
        {
            nonPhysicalHand.enabled = false;
        }*/
    }

    void FixedUpdate()
    {
        //position
        rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;

        //rotation
        Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);

        Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;

        rb.angularVelocity = (rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);

        checkForClench();
    }

    //fist attack functions

    private void checkForClench()
    {
        if(handPresence.GetComponent<HandPresence>().targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            isClenched = true;
            timeClenched += 0.02f;
        }

        else
        {
            isClenched = false;
            timeClenched = 0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if((collision.gameObject.tag == "Enemy") && isClenched)
        {
            collision.gameObject.GetComponent<LaunchEnemy>().launchByFist(timeClenched);
        }
    }

}

/*
 * Fist Attack
 * 
 * must track when either fist is clenched and for how long
 * 
 * must detect collision with an enemy while fist is clenched
 * 
 * must launch the opponent depending on the time of a fist clenched
 * 
 * Public variables:
 * the controller for each hand
 * 
 * private variables:
 * float for time fist is clenched
 * bool for if a fist is clenched
 * 
 * functions needed:
 * check if fist is clenched
 *     should update timeClenched and isClenched
 * collision detection function
 *     only if enemy is collided and isClenched is true
 *     calls a "launch by fist" function on collided object
 * 
 * 
 * 
 */