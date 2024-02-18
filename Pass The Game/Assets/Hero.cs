using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hero : MonoBehaviour
{
    private Queue<Vector3> destinationQueue = new Queue<Vector3>();
    private bool isWalking = false;

    public float speed = 5f;
    public float turnRate = 500;
    
    private float graceAngle = 30f;
    private float closeDistanceThreshold = 0.05f;
    private float turnThreshold = 30f;
    
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
    }
    
    public void MoveTo(Vector3 destination)
    {
        destinationQueue.Clear();
        destinationQueue.Enqueue(destination);

        if (!isWalking)
        {
            // Check if the hero is already close to the destination
            if (Vector3.Distance(transform.position, destination) <= closeDistanceThreshold)
            {
                transform.position = destination;
            }
            else
            {
                StartCoroutine(ProcessDestinationQueue());
            }
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(MoveToCoroutine(destination));
        }
    }
    
    private IEnumerator ProcessDestinationQueue()
    {
        while (destinationQueue.Count > 0)
        {
            Vector3 nextDestination = destinationQueue.Dequeue();
            yield return StartCoroutine(MoveToCoroutine(nextDestination));
        }

        isWalking = false;
    }
    
    private IEnumerator MoveToCoroutine(Vector3 destination)
    {
        isWalking = true;
        navMeshAgent.isStopped = true;

        yield return StartCoroutine(WaitForRotateToTarget(destination));
        
        yield return StartCoroutine(GotoDestination(destination));
        
        isWalking = false;
    }

    private IEnumerator WaitForRotateToTarget(Vector3 destination)
    {
        Vector3 targetDirection = new Vector3(destination.x - transform.position.x, 0f, destination.z - transform.position.z).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        
        while (!IsFacingTarget(destination))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnRate * Time.deltaTime);
            yield return null;
        }
        
        transform.rotation = targetRotation;
    }

    private IEnumerator GotoDestination(Vector3 destination)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(destination);
        
        while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > closeDistanceThreshold)
        {
            yield return null;
        }

        if (navMeshAgent.remainingDistance <= closeDistanceThreshold)
        {
            Vector3 destination_pos = new Vector3(destination.x, transform.position.y, destination.z);
            transform.position = destination_pos;
        }
        
        navMeshAgent.isStopped = true;
    }
    
    private bool IsFacingTarget(Vector3 targetPosition)
    {
        Vector3 directionToTarget = targetPosition - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToTarget);
        
        return Mathf.Abs(angle) <= graceAngle;
    }

    public void UseSpellSlot()
    {
        // Add your spell logic here
    }
}
