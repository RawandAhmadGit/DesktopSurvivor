using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public class EnemyStatTupel
{
    public string name;
    public float hp;
    public float attack;
    public float speed;
    public float xp;
    public float knockback;
}

public class DS_Data
{
    private DS_Data _instance;
    [SerializeField]
    private TextAsset refToEnemyDataCSV;
    private List<EnemyStatTupel> enemyEntries = new List<EnemyStatTupel>();
    private TextAsset refToPhaseDataCSV;

    private TextAsset refToWeaponData;
    private List<WeaponStatsTupel> weaponStats = new List<WeaponStatsTupel>();

    //more refs to weapon data

    internal EnemyStatTupel getEnemyOfName(string incomingName)
    {
        foreach (EnemyStatTupel entry in enemyEntries)
        {
            if (entry.name == incomingName)
            {
                return entry;
            }
        }
        Debug.Log("Did NOT find an enemy for name \"" + incomingName + "\". Please fix phase entries!\nReturning first entry available");
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

    static WeaponStatsTupel getWeaponEntry(weapontype type, int level)
    {
        foreach(WeaponStatsTupel tupel in _instance.weaponStats)
        {
            if (tupel.type == type && tupel.level == level) return tupel;
        }
        Debug.Log("getWeaponEntry() no weapon found! Returning firts entry");
        return weaponStats[0];
    }

    DS_Data()
    {
        FileStream fs = new("Assets/dataressources/Enemystats.csv", FileMode.Open);
        StreamReader sr =  new (fs);
        List<string[]> parsed = CSVSerializer.ParseCSV(sr.ReadToEnd());
        sr.Close();
        fs.Close();
        for (int i = 1; i < parsed.Count; i++)
        {
            this.enemyEntries.Add(new EnemyStatTupel());
            enemyEntries.Last().name = parsed[i][0];
            enemyEntries.Last().hp = float.Parse(parsed[i][1], CultureInfo.InvariantCulture);
            enemyEntries.Last().attack = float.Parse(parsed[i][2], CultureInfo.InvariantCulture);
            enemyEntries.Last().xp = float.Parse(parsed[i][3], CultureInfo.InvariantCulture);
            enemyEntries.Last().speed = float.Parse(parsed[i][4], CultureInfo.InvariantCulture);
            enemyEntries.Last().knockback = float.Parse(parsed[i][5], CultureInfo.InvariantCulture);

        };
        fs = new FileStream("Assets/dataressources/weaponstats.csv", FileMode.Open);
        sr = new (fs);
        List<string[]> weaponCSV = CSVSerializer.ParseCSV(sr.ReadToEnd());
        sr.Close();
        fs.Close();
        for (int i = 1; i< weaponCSV.Count; i++)
        {
            weaponStats.Add(new WeaponStatsTupel(weaponCSV[i]));
        }
        
    }

}