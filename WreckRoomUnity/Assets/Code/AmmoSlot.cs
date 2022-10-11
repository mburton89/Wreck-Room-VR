using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSlot : MonoBehaviour
{
    [SerializeField] GameObject ammoPrefab;
    GameObject currentAmmo;
    bool canRespawn = true;

    void Start()
    {
        RespawnAmmo();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentAmmo && canRespawn)
        {
            StartCoroutine(DelayedRespawn());
        }
    }

    void RespawnAmmo()
    {
        currentAmmo = Instantiate(ammoPrefab, transform.position, transform.rotation, null);
    }

    private IEnumerator DelayedRespawn()
    {
        canRespawn = false;
        yield return new WaitForSeconds(5f);
        RespawnAmmo();
        canRespawn = true;
    }
}
