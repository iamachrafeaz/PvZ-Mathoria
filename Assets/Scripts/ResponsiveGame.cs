using UnityEngine;

public class ResponsiveGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (sr == null || sr.sprite == null) return;

        // Get the world dimensions of the screen based on the camera
        float worldHeight = Camera.main.orthographicSize * 2f;
        float worldWidth = worldHeight * Camera.main.aspect;

        // Get the size of the sprite in world units
        float spriteHeight = sr.sprite.bounds.size.y;
        float spriteWidth = sr.sprite.bounds.size.x;

        // Calculate scale needed
        Vector3 scale = transform.localScale;
        scale.x = worldWidth / spriteWidth;
        scale.y = worldHeight / spriteHeight;

        transform.localScale = scale;
    }
}
