using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Spawns zombies at random spawn points with weighted probabilities for each zombie type.
// Tracks the number of zombies spawned and updates a progress bar.
public class ZombieSpawner : MonoBehaviour
{
    public Transform[] spawnpoints;         // Array of possible spawn positions
    public GameObject zombie;               // Zombie prefab to spawn
    public ZombieTypeProb[] ZombieTypes;    // Array of zombie types with their probabilities

    private List<ZombieType> probList = new List<ZombieType>(); // Weighted list for random selection

    public int zombieMax;                   // Maximum number of zombies to spawn
    public int zombiesSpawned;              // Number of zombies spawned so far

    [SerializeField] GameObject ZombieNPC;  // Parent object for spawned zombies (not used in this code)
    public float zombieDelay = 5;           // Delay between zombie spawns

    public Slider progressBar;              // UI progress bar to show spawn progress

    // Initializes the spawner, sets up the probability list, and starts spawning zombies
    private void Start()
    {
        InvokeRepeating("SpawnZombie", 4, zombieDelay);

        // Fill the probability list based on each type's probability value
        foreach (ZombieTypeProb zom in ZombieTypes)
        {
            for (int i = 0; i < zom.probability; i++)
            {
                probList.Add(zom.type);
            }
        }

        progressBar.maxValue = zombieMax;
    }

    // Updates the progress bar value each frame
    private void Update()
    {
        progressBar.value = zombiesSpawned;
    }

    // Spawns a zombie at a random spawn point with a random type, if the max hasn't been reached
    void SpawnZombie()
    {
        if (zombiesSpawned >= zombieMax)
            return;
        zombiesSpawned++;
        int r = Random.Range(0, spawnpoints.Length);
        GameObject myZombie = Instantiate(zombie, spawnpoints[r].position, Quaternion.identity, ZombieNPC.transform);
        myZombie.GetComponent<Zombie>().type = probList[Random.Range(0, probList.Count)];
    }
}


// Serializable class for defining a zombie type and its spawn probability.
[System.Serializable]
public class ZombieTypeProb
{
    public ZombieType type;     // The zombie type (ScriptableObject or class)
    public int probability;     // The relative probability weight for this type
}

