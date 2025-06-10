using UnityEngine;


// Controls the behavior of a sun object in the game.
// Suns fall to a target Y position and are destroyed after a random time.
public class Sun : MonoBehaviour
{
    public float dropToYpos;         // The Y position the sun should fall to

    private float speed = .80f;      // Falling speed

    // Called when the sun is spawned
    private void Start()
    {
        // Destroy the sun after a random time between 6 and 12 seconds
        Destroy(gameObject, Random.Range(6f, 12f));
    }

    // Handles the falling movement each frame
    private void Update()
    {
        if (transform.position.y > dropToYpos)
        {
            transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        }
    }
}
