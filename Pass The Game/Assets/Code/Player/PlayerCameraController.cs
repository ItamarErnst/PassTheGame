using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public Transform player_hero;

    public GameObject camera_holder;
    private bool followHero = true;
    
    private void Update()
    {
        CameraMovement();
    }
    
    private void CameraMovement()
    {
        float targetZoom = camera_holder.transform.position.y;

        // Check if F1 key is pressed
        if (Input.GetKeyDown(KeyCode.F1))
        {
            followHero = !followHero;

            // If following the hero, jump to the hero's position
            if (followHero && player_hero != null)
            {
                Vector3 targetPosition = new Vector3(player_hero.position.x, targetZoom, player_hero.position.z);
                camera_holder.transform.position = targetPosition;
            }
        }

        // Update camera position to follow the hero
        if (followHero && player_hero != null)
        {
            Vector3 targetPosition = new Vector3(player_hero.position.x, targetZoom, player_hero.position.z);
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