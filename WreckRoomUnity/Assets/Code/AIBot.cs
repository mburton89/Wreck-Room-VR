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
        yield return new WaitForSeconds(3);
        SetNewTarget();
        canThrow = true;
    }
}
