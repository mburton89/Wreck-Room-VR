using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBot : MonoBehaviour
{
    public float throwSpeed;
    public float maxMovementSpeed;
    public Animator animator;
    public NavMeshAgent navMeshAgent;
    Vector3 destination;
    public Transform targetToThrowAt;
    public List<GameObject> ammo;
    public Transform ammoSpawnPoint;

    bool canThrow = true;

    void Start()
    {
        SetNewTarget();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, destination) < 1)
        {
            animator.SetBool("isRunning", false);
            if (canThrow)
            {
                ThrowObjectAtPlayer();
            }
        }
        else
        {
            animator.SetBool("isRunning", true);
        }
    }

    public void SetNewTarget()
    {
        navMeshAgent.speed = maxMovementSpeed;
        int rand = Random.Range(0, AIBotManager.Instance.botDestinations.Count);
        destination = AIBotManager.Instance.botDestinations[rand].position;
        navMeshAgent.SetDestination(destination);
    }

    void ThrowObjectAtPlayer()
    {
        StartCoroutine(ThrowObjectAtPlayerCo());
    }

    private IEnumerator ThrowObjectAtPlayerCo()
    {
        canThrow = false;
        transform.LookAt(targetToThrowAt);
        yield return new WaitForSeconds(0.5f);
        transform.LookAt(targetToThrowAt);
        animator.SetTrigger("throw");
        yield return new WaitForSeconds(0.85f);
        LaunchAmmo();
        yield return new WaitForSeconds(2.5f);
        SetNewTarget();
        canThrow = true;
    }

    void LaunchAmmo()
    {
        Vector3 directionTowardPlayer = (targetToThrowAt.position - ammoSpawnPoint.position).normalized;
        int rand = Random.Range(0, ammo.Count);
        GameObject newAmmo = Instantiate(ammo[rand], ammoSpawnPoint.position, ammoSpawnPoint.rotation, null);
        newAmmo.GetComponent<Rigidbody>().AddForce(directionTowardPlayer * throwSpeed, ForceMode.Impulse);
        newAmmo.GetComponent<Rigidbody>().AddForce(Vector3.up * 1.6f, ForceMode.Impulse);
        newAmmo.GetComponent<Ammo>().hasBeenThrown = true;
    }
}
