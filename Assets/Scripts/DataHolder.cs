using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
    public TextAsset refToPhaseDataCSV;

    public TextAsset refToWeaponData;
    public List<WeaponEntry> weaponEntries = new List<WeaponEntry>();

    //more refs to weapon data

    internal EnemyEntry getEnemyOfName(string incomingName)
    {
        foreach (EnemyEntry entry in enemyEntries)
        {
            if (entry.name == incomingName)
            {
                return entry;
            }
        }
        print("Did NOT find an enemy for name \"" + incomingName + "\". Please fix phase entries!\nReturning first entry available");
        return enemyEntries[0];
    }

    internal List<MapPhaseEntry> GetPhaseEntriesOfLevel(int level)
    {
        List<string[]> csv = CSVSerializer.ParseCSV(refToPhaseDataCSV.text);
        List<MapPhaseEntry> r = new List<MapPhaseEntry>();
        for (int i = 1; i < csv.Count; i++)
        {
            r.Add(new MapPhaseEntry());
            r.Last().phase = int.Parse(csv[i][0]);
            r.Last().enemy = csv[i][1];
            r.Last().weight = int.Parse(csv[i][2]);

        }
        return r;
    }

    void Awake()
    {
        List<string[]> parsed = CSVSerializer.ParseCSV(refToEnemyDataCSV.text);
        for (int i = 1; i < parsed.Count; i++)
        {
            this.enemyEntries.Add(new EnemyEntry());
            enemyEntries.Last().name = parsed[i][0];
            enemyEntries.Last().hp = float.Parse(parsed[i][1], CultureInfo.InvariantCulture);
            enemyEntries.Last().attack = float.Parse(parsed[i][2], CultureInfo.InvariantCulture);
            enemyEntries.Last().xp = float.Parse(parsed[i][3], CultureInfo.InvariantCulture);
            enemyEntries.Last().speed = float.Parse(parsed[i][4], CultureInfo.InvariantCulture);
            enemyEntries.Last().knockback = float.Parse(parsed[i][5], CultureInfo.InvariantCulture);

        };
        List<string[]> weaponCSV = CSVSerializer.ParseCSV(refToWeaponData.text);
        for (int i = 1; i< weaponCSV.Count; i++)
        {
            weaponEntries.Add(new WeaponEntry(weaponCSV[i]));
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}