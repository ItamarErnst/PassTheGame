using UnityEngine;
using UnityEngine.Events;

public static class PlayerEvents
{
    public static UnityEvent<Vector3> OnGroundClick = new();
    public static UnityEvent<Transform> OnEnemyClick = new();
    
    public static UnityEvent<KeyCode> on_player_click_spell_slot = new();

}