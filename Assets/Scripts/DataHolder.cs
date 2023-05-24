using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
                    hp = float.Parse(parsed[i][1],CultureInfo.InvariantCulture),
                    attack = float.Parse(parsed[i][2],CultureInfo.InvariantCulture),
                    xp = float.Parse(parsed[i][3], CultureInfo.InvariantCulture),
                    speed = float.Parse(parsed[i][4], CultureInfo.InvariantCulture),
                    knockback = float.Parse(parsed[i][5], CultureInfo.InvariantCulture)
                });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
