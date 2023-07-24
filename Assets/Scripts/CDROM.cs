using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDROM : MonoBehaviour
{
    GameObject targetEnemy;
    Vector2 flyingDirection;
    PlayerAttack playerAttack;
    // Start is called before the first frame update
    void Start()
    {
        playerAttack = gameObject.GetComponent<PlayerAttack>();
        AquireRandomTarget();
        flyingDirection = (targetEnemy.transform.position - gameObject.transform.position).normalized;
    }

    private void AquireRandomTarget()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        targetEnemy = allEnemies[Random.Range(0, allEnemies.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 frameMove = flyingDirection.normalized;
        frameMove *= (playerAttack.wData.projectileSpeed * Time.deltaTime);
        transform.position += frameMove;
        transform.Rotate(Vector3.forward, 720 * Time.deltaTime);
        if (targetEnemy == null)
        {
            AquireRandomTarget();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Trigger Enter called");
        if (collision.gameObject == targetEnemy)
        {
            playerAttack.hitEnemies.Clear();
            playerAttack.RegisterHitEnemy(targetEnemy.GetComponent<GenericEnemy>());
            AquireRandomTarget();
            flyingDirection = (targetEnemy.transform.position - gameObject.transform.position).normalized;
            
        }
    }
}
