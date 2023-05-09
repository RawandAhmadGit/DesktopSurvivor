using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public float timeSinceLastSpawn = 0f;
    public Object toBeSpawned;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeSinceLastSpawn >= 4f)
        {
            Instantiate(toBeSpawned,new Vector3(Random.Range(-3,3),Random.Range(-3,3),0),Quaternion.identity);
            timeSinceLastSpawn = 0f;
        }
        else
        {
            timeSinceLastSpawn += Time.deltaTime;
        }
    }
}
