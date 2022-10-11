using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchEnemy : MonoBehaviour
{
    public Rigidbody rb;
    public AudioSource punchSound;
    private bool gettingLaunched = false;
    private Vector3 fwd;
    private float punchPower;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if(gettingLaunched)
        {
            transform.Translate(fwd * punchPower * Time.deltaTime);
        }
    }

    public void launchByFist(float timeFistClenched, Transform fistAuraTransfrom)
    {
        punchPower = timeFistClenched;
        punchSound.Play();
        fwd = fistAuraTransfrom.TransformDirection(Vector3.forward);
        StartCoroutine(stopLaunch(punchPower));
    }

    IEnumerator stopLaunch(float punchPower)
    {
        gettingLaunched = true;
        rb.useGravity = false;
        yield return new WaitForSeconds(punchPower / 2);
        gettingLaunched = false;
        rb.useGravity = true;
    }
}
