using UnityEngine;
using UnityEngine.UI;
using TMPro;


// Represents a slot in the UI for selecting and buying a plant.
// Handles displaying the plant's icon, price, and purchase logic.
public class PlantSlot : MonoBehaviour
{
    public Sprite planSprite;               // The sprite/icon for the plant
    public GameObject planObject;           // The plant prefab to instantiate
    public int price;                       // The price of the plant

    public Image icon;                      // UI image for the plant icon
    public TextMeshProUGUI priceText;       // UI text for the plant price
    private GameManager gms;                // Reference to the GameManager

    // Initializes the slot and sets up the button listener
    private void Start()
    {
        gms = GameObject.Find("GameManager").GetComponent<GameManager>();
        GetComponent<Button>().onClick.AddListener(BuyPlant);
    }

    // Handles buying the plant if the player has enough suns and no plant is currently selected
    private void BuyPlant()
    {
        if (gms.getSuns() >= price && !gms.currentPlant)
        {
            gms.setSuns(-price);
            gms.BuyPlant(planObject, planSprite);
        }
    }

    // Updates the icon and price in the editor when values change
    private void OnValidate()
    {
        if (planSprite)
        {
            icon.enabled = true;
            icon.sprite = planSprite;
            priceText.text = price.ToString();
        }
        else
        {
            icon.enabled = false;
        }
    }
}
