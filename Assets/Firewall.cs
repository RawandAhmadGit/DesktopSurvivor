using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firewall : MonoBehaviour
{
    private PlayerAttack playerAttack;
    private Vector3 _flyingDirection;
    private float _tickCD;
    // Start is called before the first frame update

    private void Awake()
    {
        
    }
    void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
        float direction = Random.Range(0f,2f * Mathf.PI);
        _flyingDirection = Vector3.zero;
        _flyingDirection.x = Mathf.Cos(direction);
        _flyingDirection.y = Mathf.Sin(direction);
        _flyingDirection *= playerAttack.wData.projectileSpeed;
        Vector3 scale = transform.localScale;
        scale.x *= playerAttack.wData.size1;
        scale.y *= playerAttack.wData.size2;
        transform.localScale = scale;
        transform.Rotate(new Vector3(0, 0, (Mathf.Rad2Deg * direction)-90));
        transform.Translate(0, 0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        transform.Translate(_flyingDirection * Time.deltaTime,Space.World);
        _tickCD -= Time.deltaTime;
        if (_tickCD <= 0)
        {
            _tickCD = 0.25f;
            gameObject.GetComponent<Collider2D>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerAttack.hitEnemies.Clear();
    }
}
