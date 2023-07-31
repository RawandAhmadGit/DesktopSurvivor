using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public class EnemyStatTupel {
    public string name;
    public float hp;
    public float attack;
    public float speed;
    public float xp;
    public float knockback;
}

public enum StatType {
    undefined,
    damage,
    attackSpeed,
    projectileSpeed,
    projectileCount,
    projectileDuration,
    projectileSize,
    armor,
    hp,
    healthRegen
}

public class DS_Data {
    private static DS_Data _instance;
    private static DS_Data GetInstance() {
        if (_instance == null) {
            _instance = new DS_Data();
        }
        return _instance;
    }
    [SerializeField]
    private List<EnemyStatTupel> enemyEntries = new();
    private List<WeaponStatsTupel> weaponStats = new();

    //more refs to weapon data

    public static EnemyStatTupel GetEnemyOfName(string incomingName) {
        GetInstance();
        foreach (EnemyStatTupel entry in _instance.enemyEntries) {
            if (entry.name == incomingName) {
                return entry;
            }
        }
        Debug.Log("Did NOT find an enemy for name \"" + incomingName + "\". Please fix phase entries!\nReturning first entry available");
        return _instance.enemyEntries[0];
    }

    public static List<MapPhaseEntry> GetPhaseEntriesOfLevel(int level) {
        GetInstance();
        FileStream fs = new("Assets/dataressources/level" + level + "genericSpawns.csv", FileMode.Open);
        StreamReader sr = new(fs);
        List<string[]> csv = CSVSerializer.ParseCSV(sr.ReadToEnd());
        sr.Close();
        fs.Close();
        List<MapPhaseEntry> r = new();
        for (int i = 1; i < csv.Count; i++) {
            r.Add(new MapPhaseEntry());
            r.Last().phase = int.Parse(csv[i][0]);
            r.Last().enemy = csv[i][1];
            r.Last().weight = int.Parse(csv[i][2], NumberStyles.Integer);

        }
        return r;
    }

    public static WeaponStatsTupel GetWeaponEntry(weapontype type, int level) {
        foreach (WeaponStatsTupel tupel in GetInstance().weaponStats) {
            if (tupel.type == type && tupel.level == level) return tupel;
        }
        Debug.Log("getWeaponEntry() no weapon found! Returning firts entry");
        return _instance.weaponStats[0];
    }

    DS_Data() {
        FileStream fs = new("Assets/dataressources/Enemystats.csv", FileMode.Open);
        StreamReader sr = new(fs);
        List<string[]> parsed = CSVSerializer.ParseCSV(sr.ReadToEnd());
        sr.Close();
        fs.Close();
        for (int i = 1; i < parsed.Count; i++) {
            this.enemyEntries.Add(new EnemyStatTupel());
            enemyEntries.Last().name = parsed[i][0];
            enemyEntries.Last().hp = float.Parse(parsed[i][1], CultureInfo.InvariantCulture);
            enemyEntries.Last().attack = float.Parse(parsed[i][2], CultureInfo.InvariantCulture);
            enemyEntries.Last().xp = float.Parse(parsed[i][3], CultureInfo.InvariantCulture);
            enemyEntries.Last().speed = float.Parse(parsed[i][4], CultureInfo.InvariantCulture);
            enemyEntries.Last().knockback = float.Parse(parsed[i][5], CultureInfo.InvariantCulture);

        };
        fs = new FileStream("Assets/dataressources/weaponstats.csv", FileMode.Open);
        sr = new(fs);
        List<string[]> weaponCSV = CSVSerializer.ParseCSV(sr.ReadToEnd());
        sr.Close();
        fs.Close();
        for (int i = 1; i < weaponCSV.Count; i++) {
            weaponStats.Add(new WeaponStatsTupel(weaponCSV[i]));
        }

    }

}