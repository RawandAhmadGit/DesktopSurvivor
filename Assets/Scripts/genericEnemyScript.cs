using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum enemyType
{
    //fill this with all kinds of enemies
    undefined = 0,
    worm1,
    worm2,
    worm3,
    virus1,
    virus2,
    virus3,
    bacteria1,
    bacteria2,
    bacteria3,
    loveLetter1,
    loveLetter2,
    trojan1,
    trojan2,
    trojan3,
    skull1,
    skull2,
    skull3
};


public class genericEnemyScript : MonoBehaviour
{
    public GameObject thePlayer;
    private float speed = 1f;
    private enemyType _type = enemyType.undefined;
    private float _maxHP = 1;
    private float _currentHP  = 1; //just to make sure they dont perish on frame 1
    private float _missingHP() { return _maxHP - _currentHP; }
    public float percentageHP() { return _currentHP / _maxHP; }
    private float _attackStrength { get; }
    private float _attackCooldown;

    

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = thePlayer.GetComponent<Transform>().position;
        Vector3 connectionLine = target - gameObject.transform.position;
        connectionLine.Normalize();
        connectionLine.Scale(new Vector3(Time.deltaTime * speed, Time.deltaTime * speed, Time.deltaTime * speed));
        gameObject.transform.Translate(connectionLine);
        
        //TODO check collision with a projectile

        //TODO perish

    }

    void defineType(enemyType newType, bool isBoss)
    {
        _type = newType;
        this._maxHP = 20;
        if (isBoss)_maxHP *= 10;
        _currentHP = _maxHP;
        //TODO
    }
}
