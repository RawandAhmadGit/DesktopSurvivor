using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDROM : MonoBehaviour
{
    GameObject targetEnemy;
    Vector2 flyingDirection;
    playerAttack PlayerAttack;
    // Start is called before the first frame update
    void Start()
    {
        PlayerAttack = gameObject.GetComponent<playerAttack>();
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        targetEnemy = allEnemies[Random.Range(0, allEnemies.Length)];
        flyingDirection = targetEnemy.transform.position - gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 frameMove = flyingDirection.normalized;
        frameMove *= (PlayerAttack.wData.projectileSpeed * Time.deltaTime);
        transform.position += frameMove;
        transform.Rotate(Vector3.forward, 720 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
