using UnityEngine;

// Spawns sun objects at random positions and intervals.
// Each sun falls to a random Y position and is destroyed after a set time.
public class SunSpawner : MonoBehaviour
{
    public GameObject sunObject; // Prefab for the sun object

    // Called when the spawner starts
    private void Start()
    {
        SpawnSun();
    }

    // Spawns a sun at a random position and schedules the next spawn
    private void SpawnSun()
    {
        // Instantiate sun at a random X position, fixed Y (top of screen), and Z=0
        GameObject mySun = Instantiate(sunObject, new Vector3(Random.Range(-4f, 8.35f), 6, 0), Quaternion.identity);
        // Set the target Y position for the sun to fall to
        mySun.GetComponent<Sun>().dropToYpos = Random.Range(2f, -3f);
        // Schedule the next sun spawn after a random interval
        Invoke("SpawnSun", Random.Range(4, 10));
    }
}
