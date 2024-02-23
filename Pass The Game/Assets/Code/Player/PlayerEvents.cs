using UnityEngine;
using UnityEngine.Events;

public static class PlayerEvents
{
    public static UnityEvent<Vector3> OnGroundClick = new();
    public static UnityEvent<Transform> OnEnemyClick = new();
    public static UnityEvent<int> OnSpellSelect = new();
    
    public static UnityEvent OnReachedDestination = new();
    public static UnityEvent<Spell> OnSpellSelected = new();

}