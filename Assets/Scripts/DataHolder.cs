using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntry
{
    public string name;
    public float hp;
    public float attack;
    public float speed;
    public float xp;
    public float knockback;
}

public class DataHolder : MonoBehaviour
{
    [SerializeField]
    public TextAsset refToEnemyDataCSV;
    public List<EnemyEntry> enemyEntries = new List<EnemyEntry>();


    void Start()
    {
        List<string[]> parsed = CSVSerializer.ParseCSV(refToEnemyDataCSV.text);
        for (int i = 1; i < parsed.Count; i++)
        {
            enemyEntries.Add(
                new EnemyEntry{
                    name = parsed[i][0],
                    hp = float.Parse(parsed[i][1]),
                    attack = float.Parse(parsed[i][2]),
                    xp = float.Parse(parsed[i][3]),
                    speed = float.Parse(parsed[i][4]),
                    knockback = float.Parse(parsed[i][5])});
        }
        foreach (EnemyEntry e in enemyEntries)
        print(e.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
