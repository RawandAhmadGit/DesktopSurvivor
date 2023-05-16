using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public float timeSinceLastSpawn = 0f;
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
            if (timeSinceLastSpawn >= 4f)
            {
                Instantiate(toBeSpawned, new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0), Quaternion.identity);
                timeSinceLastSpawn = 0f;
            }
            else
            {
                timeSinceLastSpawn += Time.deltaTime;
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
