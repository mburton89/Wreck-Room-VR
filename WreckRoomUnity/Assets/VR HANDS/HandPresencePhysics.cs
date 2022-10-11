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
    [HideInInspector] public bool isClenched = false;
    [HideInInspector] public float timeClenched = 0;
    public GameObject fistAuraPrefab;
    public Transform fistAuraTargetPosition;
    private GameObject fistAura;
    private bool auraSpawned = false;
    public AudioSource chargingSound;

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
        
        //fist attack stuff
        checkForClench();
        if(auraSpawned)
        {
            fistAura.GetComponent<Rigidbody>().velocity = (fistAuraTargetPosition.position - fistAura.transform.position) / Time.fixedDeltaTime;
            fistAura.GetComponent<Rigidbody>().angularVelocity = (rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
        }
    }

    //fist attack functions

    private void checkForClench()
    {
        if(handPresence.GetComponent<HandPresence>().gripPressed)
        {
            isClenched = true;
            
            if(timeClenched < 10f)
            {
                timeClenched += 0.02f;
            }

            if(timeClenched > 10f)
            {
                timeClenched = 10f;
            }
            
            if(auraSpawned == false)
            {
                fistAura = Instantiate(fistAuraPrefab, rb.position, rb.rotation);
                fistAura.GetComponent<FistAura>().handPhysics = this;
                auraSpawned = true;
                chargingSound.Play();
            }
        }

        else
        {
            isClenched = false;
            timeClenched = 0f;
            Destroy(fistAura);
            auraSpawned = false;
            chargingSound.Stop();
        }
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if((collision.gameObject.tag == "Enemy") && isClenched)
        {
            collision.gameObject.GetComponent<LaunchEnemy>().launchByFist(timeClenched);
            timeClenched = 0f;
        }
    }
    */
}
