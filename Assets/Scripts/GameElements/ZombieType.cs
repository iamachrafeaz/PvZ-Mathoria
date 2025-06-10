using UnityEngine;


// ScriptableObject that defines the stats and appearance for a zombie type.
// Used to configure different zombie variants in the game.
[CreateAssetMenu(fileName = "New ZombieType", menuName = "Zombie")]
public class ZombieType : ScriptableObject
{
    public int health;                  // The zombie's maximum health
    public float speed;                 // Movement speed
    public int damage;                  // Damage dealt to plants
    public float range = .5f;           // Range to detect plants
    public float eatCooldown = 1f;      // Cooldown between eating actions
    public Sprite sprite;               // Sprite for the zombie's normal appearance
    public Sprite deathSprite;          // Sprite to use when the zombie dies

    public string walkAnimationName;    // Name of the walk animation in the Animator
}
