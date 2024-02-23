using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Hero hero;
    
    public MovementController movementController;
    public AttackController attackController;

    public Transform target;
    
    void Start()
    {
        movementController.SetHeroSpeed(hero.speed);

        PlayerEvents.OnGroundClick.AddListener(MoveToDestination);
        PlayerEvents.OnEnemyClick.AddListener(SetEnemyTarget);
    }

    void Update()
    {
        if (target != null)
        {
            float distanceToTarget = movementController.GetDistanceFrom(target);

            if (distanceToTarget <= attackController.attackRange)
            {
                movementController.StopMoving();
                attackController.PerformAttack();
            }
            else
            {
                if (!movementController.IsWalking())
                {
                    movementController.MoveTo(target.position);
                }
            }
        }
    }

    private void MoveToDestination(Vector3 destination)
    {
        SetTarget(null);
        movementController.MoveTo(destination);
    }

    private void SetEnemyTarget(Transform enemy)
    {
        SetTarget(enemy);
    }

    private void SetTarget(Transform newTarget)
    {
        target = newTarget;

        if (target == null)
        {
            attackController.StopAttack();
        }
    }
}