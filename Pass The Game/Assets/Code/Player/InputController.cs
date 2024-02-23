using UnityEngine;

public class InputController : MonoBehaviour
{
    private Camera mainCamera;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;

    public Pointer pointer;

    void Start()
    {
        mainCamera = Camera.main;
    }
    
    private void Update()
    {
        MovementInputCheck();
        SkillInputsCheck();
    }

    private void MovementInputCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayer))
            {
                HandleTargetClick(hit);
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                HandleGroundClick(hit);
            }
        }
    }
    
    private void SkillInputsCheck()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerEvents.on_player_click_spell_slot.Invoke(KeyCode.Q);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            PlayerEvents.on_player_click_spell_slot.Invoke(KeyCode.W);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerEvents.on_player_click_spell_slot.Invoke(KeyCode.E);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerEvents.on_player_click_spell_slot.Invoke(KeyCode.R);
        }
    }
    

    private void HandleTargetClick(RaycastHit hit)
    {
        PlayerEvents.OnEnemyClick.Invoke(hit.transform);
    }

    private void HandleGroundClick(RaycastHit hit)
    {
        PlayerEvents.OnGroundClick.Invoke(hit.point);
        pointer.transform.position = hit.point;
        pointer.OnPoint();
    }
}