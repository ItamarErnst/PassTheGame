using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player_hero;
    public GameObject camera_holder;
    private bool followHero = true;

    public float edgeScrollingSpeed = 10f;
    public float edgeScrollingThreshold = 125f;
    public float breakEdgeScrollingThreshold = 50f;
    
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
            followHero = true;

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

        // If arrow keys are pressed or mouse edge scrolling, stop following the hero
        if (followHero && (HasArrowKeyInput() || IsMouseEdgeScrolling(breakEdgeScrollingThreshold)))
        {
            followHero = false;
        }

        if (!followHero)
        {
            // Handle camera movement with arrow keys
            float horizontal = 0f;
            float vertical = 0f;

            // Check arrow key input
            if (HasArrowKeyInput())
            {
                horizontal = Input.GetAxis("Horizontal");
                vertical = Input.GetAxis("Vertical");
            }

            // Exclude W, A, S, D keys from horizontal and vertical movement
            if (IsMouseEdgeScrolling(edgeScrollingThreshold))
            {
                horizontal = 0f;
                vertical = 0f;
            }

            Vector3 cameraMovement = new Vector3(horizontal, 0, vertical) * Time.deltaTime * 10f;
            camera_holder.transform.Translate(cameraMovement, Space.World);

            // Handle edge scrolling with mouse
            EdgeScrolling();
        }
    }

    private bool HasArrowKeyInput()
    {
        return Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);
    }

    private bool IsMouseEdgeScrolling(float threshold)
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        return (mouseX > 0 && mouseX < threshold) ||
               (mouseX > screenWidth - threshold && mouseX < screenWidth) ||
               (mouseY > 0 && mouseY < threshold) ||
               (mouseY > screenHeight - threshold && mouseY < screenHeight);
    }

    private void EdgeScrolling()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        if (mouseX > 0 && mouseX < edgeScrollingThreshold)
        {
            camera_holder.transform.Translate(Vector3.left * edgeScrollingSpeed * Time.deltaTime, Space.World);
        }
        else if (mouseX > screenWidth - edgeScrollingThreshold && mouseX < screenWidth)
        {
            camera_holder.transform.Translate(Vector3.right * edgeScrollingSpeed * Time.deltaTime, Space.World);
        }

        if (mouseY > 0 && mouseY < edgeScrollingThreshold)
        {
            camera_holder.transform.Translate(Vector3.back * edgeScrollingSpeed * Time.deltaTime, Space.World);
        }
        else if (mouseY > screenHeight - edgeScrollingThreshold && mouseY < screenHeight)
        {
            camera_holder.transform.Translate(Vector3.forward * edgeScrollingSpeed * Time.deltaTime, Space.World);
        }
    } 
}
