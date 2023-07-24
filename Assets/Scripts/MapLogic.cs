using System.Collections.Generic;
using UnityEngine;

public class MapPhaseEntry
{
    public int phase;
    public string enemy;
    public int weight;
}

public class MapLogic : MonoBehaviour
{
    public float timeUntilNextSpawn = 2f;
    public float mapTime;
    public GameObject toBeSpawned;
    public int level;

    private bool mapUnpaused = true;

    private List<MapPhaseEntry> phaseEntries = new();

    void Start()
    {
        // Assign the prefab manually
        phaseEntries = DS_Data.GetPhaseEntriesOfLevel(level);
    }

    void Update()
    {
        if (mapUnpaused)
        {
            if (timeUntilNextSpawn <=  0)
            {
                const float spawnRadius = 10;
                Vector3 newEnemyPos = (Quaternion.AngleAxis(Random.Range(0,360),Vector3.forward) * Vector3.up) * spawnRadius;
                GameObject newestEnemy = Instantiate(toBeSpawned, newEnemyPos, Quaternion.identity);
                newestEnemy.GetComponent<GenericEnemy>().defineEnemyType(getViableEnemyEntry(),false);
                timeUntilNextSpawn = 4f;
            }
            else
            {
                timeUntilNextSpawn -= Time.deltaTime;
            }
        }
    }

    public void Unpause()
    {
        mapUnpaused = true;
    }

    public void Pause()
    {
        mapUnpaused = false;
    }

    private EnemyStatTupel getViableEnemyEntry()
    {
        //first we create a weighted list of all viable enemies for this phase
        List<MapPhaseEntry> weightedList = new();
        for (int iteratorPhaseEntries = 0;iteratorPhaseEntries < phaseEntries.Count; iteratorPhaseEntries++) { 
            if (phaseEntries[iteratorPhaseEntries].phase == getCurrentPhase())
            {
                for (int  iteratorWeightCounter = 0;iteratorWeightCounter < phaseEntries[iteratorPhaseEntries].weight; iteratorWeightCounter++)
                {
                    weightedList.Add(phaseEntries[iteratorPhaseEntries]);
                }
            }
        }
        //now we choose a random phaseEntry from the weighted lsit and get a corresponding enemyEntry
        string enemyName = weightedList[Random.Range(0, weightedList.Count)].enemy;
        return DS_Data.GetEnemyOfName(enemyName);

        //return GameObject.FindGameObjectWithTag("BalanceParameter").GetComponent<DataHolder>().enemyEntries[Random.Range(0, GameObject.FindGameObjectWithTag("BalanceParameter").GetComponent<DataHolder>().enemyEntries.Count)];
}

    private int getCurrentPhase()
    {
        return Mathf.FloorToInt(mapTime / 150f) + 1;
    }
}


