using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera mainCamera;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    
    public HpDisplay hp_display;
    public ManaDisplay mana_display;

    public GameObject camera_holder;
    private bool followHero = true;
    public Pointer pointer;
    
    public Hero hero;

    public Spell active_spell;
    
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

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayer))
            {
                HandleEnemyClick(hit);
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                HandleGroundClick(hit.point);
            }
        }
    }

    private void HandleEnemyClick(RaycastHit hit)
    {
        if (active_spell.type != SpellType.None)
        {
            UseActiveSpell(hit.point);
        }
        else
        {
            hero.SetTarget(hit.transform.gameObject);
        }
    }

    private void HandleGroundClick(Vector3 targetPoint)
    {
        if (active_spell.type != SpellType.None)
        {
            UseActiveSpell(targetPoint);
        }
        else
        {
            HandleGroundClickActions(targetPoint);
        }
    }

    private void HandleGroundClickActions(Vector3 targetPoint)
    {
        pointer.transform.position = targetPoint;
        pointer.OnPoint();
        hero.OnClickGround(targetPoint);
    }
    
    private void SkillInputsCheck()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q pressed");

            if(mana_display.HasEnough(hero.GetSpell().mana))
            {
                Debug.Log("Has Mana");

                active_spell = hero.GetSpell();
            }
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

    private void UseActiveSpell(Vector3 pos)
    {
        mana_display.Remove(active_spell.mana);

        ParticleSystem spell = Instantiate(active_spell.pr);
        spell.transform.position = pos;

        active_spell = new Spell();
    }
}