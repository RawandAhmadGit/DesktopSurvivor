using UnityEngine;

public class MapLogic : MonoBehaviour
{
    public float timeUntilNextSpawn = 0f;
    public GameObject toBeSpawned;

    private bool isSpawningEnabled = true;

    void Start()
    {
        // Assign the prefab manually
    }

    void Update()
    {
        if (isSpawningEnabled)
        {
            if (timeUntilNextSpawn <=  0)
            {
                const float spawnRadius = 10;
                Vector3 newEnemyPos = (Quaternion.AngleAxis(Random.Range(0,360),Vector3.forward) * Vector3.up) * Time.deltaTime * spawnRadius;
                Instantiate(toBeSpawned, newEnemyPos, Quaternion.identity);
                timeUntilNextSpawn = 4f;
            }
            else
            {
                timeUntilNextSpawn += Time.deltaTime;
            }
        }
    }

    public void EnableSpawning()
    {
        isSpawningEnabled = true;
    }

    public void DisableSpawning()
    {
        isSpawningEnabled = false;
    }
}
