using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchEnemy : MonoBehaviour
{
    public Rigidbody rb;
    public AudioSource punchSound;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void launchByFist(float timeFistClenched)
    {
        punchSound.Play();
    }
}