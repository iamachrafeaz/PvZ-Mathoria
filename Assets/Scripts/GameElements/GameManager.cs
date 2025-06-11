using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance;

    // Reference to the parent object for planted NPCs
    [SerializeField] GameObject PlantedNPC;

    // Currently selected plant and its sprite
    public GameObject currentPlant;
    public Sprite currentPlantSprite;

    // Parent transform containing all tile objects
    public Transform tiles;

    // Layer masks for tiles and suns
    public LayerMask tileMask;
    public LayerMask sunMask;

    // Basic amount of suns (currency)
    int suns = 100;

    // UI text displaying the number of suns
    public TextMeshProUGUI sunTEXT;

    // Singleton pattern and persistence across scenes
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist this object across scene loads
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    // Set the current plant and its sprite for planting
    public void BuyPlant(GameObject plant, Sprite sprite)
    {
        currentPlant = plant;
        currentPlantSprite = sprite;
    }

    // Enable enhanced touch support when this object is enabled
    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    // Disable enhanced touch support when this object is disabled
    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    // Main update loop for handling input and game logic
    private void Update()
    {
        // Update the sun count UI
        sunTEXT.text = suns.ToString();

        // If there is no main camera, exit early
        if (Camera.main == null) return;

        Vector2 screenPos = Vector2.zero;
        bool inputPressed = false;

        // Handle touch input
        if (Touchscreen.current != null && Touchscreen.current.touches.Count > 0)
        {
            var touch = Touchscreen.current.touches[0].ReadValue();
            screenPos = touch.position;
            inputPressed = Touchscreen.current.touches[0].press.wasPressedThisFrame;
        }
        // Handle mouse input
        else if (Mouse.current != null)
        {
            screenPos = Mouse.current.position.ReadValue();
            inputPressed = Mouse.current.leftButton.wasPressedThisFrame;
        }
        else
        {
            return; // No input device available
        }

        // Convert screen position to world position
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        // Raycast to detect tile under the cursor/touch
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, Mathf.Infinity, tileMask);

        // Hide all tile highlights
        foreach (Transform tile in tiles)
        {
            tile.GetComponent<SpriteRenderer>().enabled = false;
        }

        // If a tile is hit and a plant is selected
        if (hit.collider && currentPlant)
        {
            Tile tile = hit.collider.GetComponent<Tile>();
            if (tile != null)
            {
                // Highlight the tile with the plant sprite
                var sr = hit.collider.GetComponent<SpriteRenderer>();
                sr.sprite = currentPlantSprite;
                sr.enabled = true;

                // If input is pressed and the tile is empty, plant the plant
                if (inputPressed && !tile.hasPlant)
                {
                    Instantiate(currentPlant, hit.collider.transform.position, Quaternion.identity, PlantedNPC.transform);
                    tile.hasPlant = true;
                    currentPlant = null;
                    currentPlantSprite = null;
                }
            }
        }

        // Raycast to detect sun objects under the cursor/touch
        RaycastHit2D sunhit = Physics2D.Raycast(worldPos, Vector2.zero, Mathf.Infinity, sunMask);
        if (sunhit.collider && inputPressed)
        {
            suns += 50; // Collect sun and increase currency
            Destroy(sunhit.collider.gameObject); // Remove the collected sun object
        }
    }

    // Add a specified amount to the sun count
    public void setSuns(int amount)
    {
        suns += amount;
    }

    // Get the current sun count
    public int getSuns()
    {
        return suns;
    }

    // Pause the game (freeze time)
    public void Pause() => Time.timeScale = 0;

    // Continue the game (resume time)
    public void Continue() => Time.timeScale = 1;

    // Called when the game is finished; destroys this manager
    public void OnGameFinished()
    {
        Destroy(gameObject);
    }


}
