using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [HideInInspector] public bool hasBeenThrown = false;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") && hasBeenThrown)
        {
            Destroy(gameObject, 3);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            print("ENEMY HIT");
            audioSource.Play();
            GameManager.Instance.HandleEnemyHit();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            print("Player HIT");
            audioSource.Play();
            GameManager.Instance.GameOver();
        }
    }
}
