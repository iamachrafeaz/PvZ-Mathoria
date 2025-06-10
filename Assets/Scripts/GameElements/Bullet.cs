using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage; // How damaged the zombies will be after the bullet hits them

    public float speed = 0.8f; // Speed of the bullet 

    public bool freeze; // This is a feature available for "SnowPeaShooter" that freezes the zombie.

    void Start()
    {
        Destroy(gameObject, 10);  // Destroy the bullet after 10 seconds
    }

    void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0); // Move the bullet across the garden (Horizontally) with a speed
    }

    // Function that that detects zombies on its way, applies damage to them and destroys itself.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Zombie>(out Zombie zombie))
        {
            zombie.Hit(damage, freeze);
            Destroy(gameObject);
        }

    }
}
