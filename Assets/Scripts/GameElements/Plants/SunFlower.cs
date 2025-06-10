using UnityEngine;

public class SunFlower : MonoBehaviour
{
    public GameObject sunObject; // Prefab of the Sun 

    public float cooldown; // Duration between each instationation of the sun

    void Start()
    {
        // Call SpawnSun() every "cooldown" and destroy it after "cooldown"
        InvokeRepeating("SpawnSun", cooldown, cooldown);
    }

    // Function to instantiate the sun and position it randomly
    void SpawnSun()
    {
        GameObject mySun = Instantiate(sunObject, new Vector3(transform.position.x + Random.Range(-.0f , .5f), transform.position.y + Random.Range(-.5f, .5f), 0), Quaternion.identity );
        mySun.GetComponent<Sun>().dropToYpos = transform.position.y - 1;
    }



}
