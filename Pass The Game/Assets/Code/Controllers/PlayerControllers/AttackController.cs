using UnityEngine;

public class AttackController : MovementController
{
    public float attackRange = 5f;
    
    public void PerformAttack()
    {
        Debug.LogError("Attack");
    }

    public void StopAttack()
    {
        
    }

    public float GetAttackRange()
    {
        return attackRange;
    }
}