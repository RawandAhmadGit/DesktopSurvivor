using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpText : MonoBehaviour
{
    float lifetime;
    float risingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        lifetime = 2;
        risingSpeed = 0.75f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, risingSpeed * Time.deltaTime, 0));
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
            Destroy(gameObject);
    }
}
