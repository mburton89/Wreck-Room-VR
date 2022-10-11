using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistAura : MonoBehaviour
{
    [HideInInspector] public HandPresencePhysics handPhysics;

    private void OnTriggerEnter(Collider collider)
    {
        if ((collider.gameObject.tag == "Enemy") && handPhysics.isClenched)
        {
            collider.gameObject.GetComponent<LaunchEnemy>().launchByFist(handPhysics.timeClenched, this.transform);
            handPhysics.timeClenched = 0f;
        }
    }
}
