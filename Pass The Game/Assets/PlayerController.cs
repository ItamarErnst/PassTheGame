using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera mainCamera;
    public GameObject camera_holder;
    public LayerMask groundLayer;
    public Hero hero;
    
    private bool followHero = true;
    
    void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        SkillInputsCheck();
        MovementInputCheck();
        CameraMovement();
    }

    private void MovementInputCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log("CLICK");

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                Debug.Log("HIT GROUND");

                // Move the hero to the clicked position
                hero.MoveTo(hit.point);
            }
        }
    }

    private void SkillInputsCheck()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q pressed");
            hero.UseSpellSlot();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W pressed");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R pressed");
        }
    }

    private void CameraMovement()
    {
        float targetZoom = camera_holder.transform.position.y;

        // Check if F1 key is pressed
        if (Input.GetKeyDown(KeyCode.F1))
        {
            followHero = !followHero;

            // If following the hero, jump to the hero's position
            if (followHero && hero != null)
            {
                Vector3 targetPosition = new Vector3(hero.transform.position.x, targetZoom, hero.transform.position.z);
                camera_holder.transform.position = targetPosition;
            }
        }

        // Update camera position to follow the hero
        if (followHero && hero != null)
        {
            Vector3 targetPosition = new Vector3(hero.transform.position.x, targetZoom, hero.transform.position.z);
            camera_holder.transform.position = Vector3.Lerp(camera_holder.transform.position, targetPosition, Time.deltaTime * 5f);
        }

        // If arrow keys are pressed, stop following the hero
        if (followHero && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            followHero = false;
        }

        if (!followHero)
        {
            // Handle camera movement
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            
            Vector3 cameraMovement = new Vector3(horizontal, targetZoom, vertical) * Time.deltaTime * 10f;
            camera_holder.transform.Translate(cameraMovement, Space.World);
        }
    }

    
}