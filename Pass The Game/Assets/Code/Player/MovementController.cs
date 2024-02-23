using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Queue<Vector3> destinationQueue = new Queue<Vector3>();
    private bool isWalking = false;
    private Coroutine movementCoroutine;

    public float turnRate = 500;

    private float graceAngle = 30f;
    private float closeDistanceThreshold = 0.05f;
    private float turnThreshold = 30f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void SetHeroSpeed(float speed)
    {
        navMeshAgent.speed = speed;
    }

    public void MoveTo(Vector3 destination)
    {
        destinationQueue.Clear();
        destinationQueue.Enqueue(destination);

        if (!IsWalking())
        {
            // Check if the hero is already close to the destination
            if (Vector3.Distance(transform.position, destination) <= closeDistanceThreshold)
            {
                transform.position = destination;
            }
            else
            {
                movementCoroutine = StartCoroutine(ProcessDestinationQueue());
            }
        }
        else
        {
            StopCoroutine(movementCoroutine);
            movementCoroutine = StartCoroutine(MoveToCoroutine(destination));
        }
    }

    public void StopMoving()
    {
        StopAllCoroutines();
        navMeshAgent.isStopped = true;
        isWalking = false;
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

        yield return StartCoroutine(WaitForRotation(destination));

        yield return StartCoroutine(GotoDestination(destination));

        isWalking = false;
    }

    private IEnumerator WaitForRotation(Vector3 destination)
    {
        Vector3 targetDirection = new Vector3(destination.x - transform.position.x, 0f, destination.z - transform.position.z).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        while (!IsFacingTarget(destination))
        {
            Rotate(targetDirection);
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

    private void Rotate(Vector3 targetDirection)
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnRate * Time.deltaTime);
    }

    public float GetDistanceFrom(Transform target)
    {
        return Vector3.Distance(transform.position, target.position);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
