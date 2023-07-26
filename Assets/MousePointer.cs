using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MousePointer : MonoBehaviour {
    PlayerAttack playerAttack;
    Vector3 speed;
    GenericEnemy targetEnemy;
    // Start is called before the first frame update
    void Start() {
        playerAttack = GetComponent<PlayerAttack>();
        aquireNewTarget();
        speed = targetEnemy.transform.position - transform.position;
        speed.Normalize();
        speed *= playerAttack.wData.projectileSpeed * 0.5f;
    }

    private void aquireNewTarget() {
        GenericEnemy[] allEnemies = GameObject.FindObjectsOfType<GenericEnemy>();
        targetEnemy = allEnemies[Random.Range(0, allEnemies.Length)];
    }
    // Update is called once per frame
    void Update() {
        if (targetEnemy == null) {
            aquireNewTarget();
        }
        Vector3 connector = targetEnemy.transform.position - transform.position;
        connector.Normalize();
        speed += connector * Time.deltaTime * playerAttack.wData.projectileSpeed;
        if (speed.sqrMagnitude < 1) {
            playerAttack.hitEnemies.Clear();
        }
        transform.Translate(speed * Time.deltaTime, Space.World);
        transform.eulerAngles = new Vector3(0, 0, Vector3.SignedAngle(Vector3.up, speed, Vector3.forward));
        //transform.position = transform.transform.transform.transform.transform.transform.transform.position;
        //shitposting 
    }


}
