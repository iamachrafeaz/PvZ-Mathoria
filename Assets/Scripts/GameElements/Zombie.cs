using UnityEngine;


// Controls zombie behavior: movement, attacking plants, taking damage, and animations.
public class Zombie : MonoBehaviour
{
    private float speed;                // Current movement speed
    private int health;                 // Current health

    public float range;                 // Range to detect plants
    public ZombieType type;             // ScriptableObject holding zombie stats

    private int damage;                 // Damage dealt to plants

    public LayerMask plantMask;         // Layer mask for detecting plants

    private float eatCooldown;          // Cooldown between eating actions

    private bool canEat = true;         // Whether the zombie can currently eat

    public Plant targetPlant;           // The plant currently being attacked

    private Animator animator;          // Animator for zombie animations

    // Initializes zombie stats and animation at spawn
    private void Start()
    {
        health = type.health;
        speed = type.speed;
        damage = type.damage;
        range = type.range;
        eatCooldown = type.eatCooldown;

        GetComponent<SpriteRenderer>().sprite = type.sprite;

        animator = GetComponent<Animator>();

        if (animator != null && !string.IsNullOrEmpty(type.walkAnimationName))
        {
            animator.Play(type.walkAnimationName);
        }
    }

    // Handles plant detection and eating logic each frame
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, range, plantMask);

        if(hit.collider )
        {
            targetPlant = hit.collider.GetComponent<Plant>();
            Eat();
        }
    }

    // Damages the target plant if able to eat, and starts cooldown
    void Eat()
    {
        if(!canEat)
            return;
        canEat = false;
        Invoke("ResetEatCooldwon", eatCooldown);

        targetPlant.Hit(damage);
    }

    // Handles zombie movement if not currently eating a plant
    private void FixedUpdate()
    {
        if(!targetPlant)
            transform.position -= new Vector3(speed, 0, 0);
    }

    // Resets the eating cooldown (called by Invoke)
    void ResetEatCooldwon()
    {
        canEat = false;
    }

    // Called when the zombie takes damage; may freeze or die
    public void Hit(int damage, bool freeze)
    {
        health -= damage;
        if (freeze)
        {
            Freeze();
        }
        if (health <= 0)
        {
            GetComponent<SpriteRenderer>().sprite = type.deathSprite;
            Destroy(gameObject, 1);
        }
    }

    // Freezes the zombie for 5 seconds
    void Freeze()
    {
        CancelInvoke("UnFreeze");
        GetComponent<SpriteRenderer>().color = Color.blue;
        speed = 0;
        Invoke("UnFreeze", 5);
    }

    // Unfreezes the zombie, restoring speed and color
    void UnFreeze()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        speed = type.speed;
    }
}
