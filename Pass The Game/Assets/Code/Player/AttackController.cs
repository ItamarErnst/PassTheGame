using UnityEngine;

public class AttackController : MovementController
{
    public float attackRange = 5f;

    public void PerformAttack()
    {
        // Implement attack logic here (e.g., trigger attack animation, deal damage)
        Debug.LogError("Attack");
    }

    public void StopAttack()
    {
        // Implement logic to stop the attack (e.g., stop attack animation)
    }
}