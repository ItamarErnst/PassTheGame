using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum ActionType
{
    None,
    Moving,
    AutoAttack,
    CastingSpell,
    WaitingForSpellTarget,
}
public class PlayerController : MonoBehaviour
{
    public HeroData heroData;

    public ActionType current_action = ActionType.None;
    
    public MovementController movementController;
    public AttackController attackController;
    public SpellController spellController;
    public ResourceManager resourceManager;
    
    public Transform target;
    public Vector3 destination;

    private void Awake()
    {
        destination = movementController.transform.position;
        
        spellController.SetHero(heroData);
    }

    void Start()
    {
        movementController.SetMovementSpeed(heroData.speed);

        PlayerEvents.OnGroundClick.AddListener(MoveToDestination);
        PlayerEvents.OnEnemyClick.AddListener(SetEnemyTarget);
        PlayerEvents.OnSpellSelected.AddListener(SelectSpell);
        PlayerEvents.OnReachedDestination.AddListener(OnReachedDestination);
    }

    void Update()
    {
        if (target != null)
        {
            destination = target.position;
        }
        
        float distanceToDestination = movementController.GetDistanceFrom(destination);

        if (current_action == ActionType.CastingSpell && distanceToDestination <= spellController.GetCastRange())
        {
            current_action = ActionType.None;
            movementController.StopMoving();
            movementController.Rotate(destination);
            
            resourceManager.RemoveMana(spellController.GetSelectedSpell().mana);
            spellController.CastSelectedSpell(destination);
        }
        else if (current_action == ActionType.AutoAttack && distanceToDestination <= heroData.attack_range)
        {
            movementController.StopMoving();
            movementController.Rotate(destination);
            
            attackController.PerformAttack();
        }
        else if(current_action != ActionType.None && current_action != ActionType.WaitingForSpellTarget)
        {
            if (!movementController.IsWalking())
            {
                movementController.MoveTo(destination);
            }
        }
    }

    private void MoveToDestination(Vector3 destination)
    {
        if (current_action != ActionType.WaitingForSpellTarget)
        {
            current_action = ActionType.Moving;
        }
        else
        {
            current_action = ActionType.CastingSpell;
        }
        
        SetTarget(null);
        this.destination = destination;
        movementController.MoveTo(destination);
    }

    private void SetEnemyTarget(Transform enemy)
    {
        if (current_action != ActionType.WaitingForSpellTarget)
        {
            current_action = ActionType.AutoAttack;
        }
        else
        {
            current_action = ActionType.CastingSpell;
        }
        
        SetTarget(enemy);
    }
    
    private void SelectSpell(Spell spell)
    {
        if(spell.type == SpellType.None) return;
        
        if (resourceManager.HasManaFor(spell.mana))
        {
            current_action = ActionType.WaitingForSpellTarget;
        }
    }
    
    private void OnReachedDestination()
    {
        if (current_action == ActionType.Moving)
        {
            current_action = ActionType.None;
        }
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