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
            PlayerEvents.OnSpellSelect.Invoke(1);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            PlayerEvents.OnSpellSelect.Invoke(2);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerEvents.OnSpellSelect.Invoke(3);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerEvents.OnSpellSelect.Invoke(4);
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