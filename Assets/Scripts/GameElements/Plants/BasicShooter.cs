using UnityEngine;

public class BasicShooter : MonoBehaviour
{
    public GameObject bullet; // Pea Preafab for PeaShooter

    public Transform shootOrigin; // Part of the plant where the pea comes out.

    public float cooldown; // Duration between each shot.

    public float range; // Detection distance of the zombie by the plant. 

    private bool canShoot = true; // Controls whether the plant is allowed to shoot or not.

    public LayerMask shootMask; // Zombie's Layer. In the editor, the layer is called "Target", which only concerns zombies' prefabs.

    void Update()
    {
        RaycastHit2D hit= Physics2D.Raycast(transform.position, Vector2.right , range , shootMask) ; // Detection Zone that works as a radar

        if(hit.collider) // If there is an GameObject where its layer is "Target" (Zombie) inside the zone, call "Shoot()" method
        {
            Shoot(); 
        }
    }

    void ResetCooldown()
    {
        canShoot = true;
    }

    // Makes the plant shoot after a cooldown 
    void Shoot()
    {
        if (!canShoot)
        {
            return; // if the plant isn't allowed to shoot, don't do anything
        }
        
        canShoot = false;

        Invoke("ResetCooldown", cooldown); // After a duration "cooldown", assign true to "canShoot" attribut

        Instantiate(bullet, shootOrigin.position, Quaternion.identity) ; // When the plant is allowed (canShoot == true), fire a bullet from the the orginal postion
    }
}
