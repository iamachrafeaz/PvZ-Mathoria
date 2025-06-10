using UnityEngine;


// Represents a plant in the game. Handles health, taking damage, and destruction.
public class Plant : MonoBehaviour
{
    public int health; // The plant's current health

    // Sets the plant's layer on start
    private void Start()
    {
        gameObject.layer = 9;
    }

    // Called when the plant takes damage
    public void Hit(int damage)
    {
        health -= damage;
        if (health < 0)
            Destroy(gameObject); // Destroy the plant if health drops below zero
    }
}
