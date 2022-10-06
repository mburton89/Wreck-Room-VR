using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchEnemy : MonoBehaviour
{
    public Rigidbody rb;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void launchByFist(float timeFistClenched)
    {
        print("I'm getting launched");
        print(timeFistClenched);
    }
}
