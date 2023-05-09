using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class playerScript : MonoBehaviour
{
    public const float baseMoveSpeed = 3f;
    public float moveSpeedStatMultiplier = 1f;
    public float debuffMoveSpeedMultiplier = 1f;
    public const float minimumSpeed = 1f;
    private float effectiveSpeed() { return math.max(baseMoveSpeed * moveSpeedStatMultiplier * debuffMoveSpeedMultiplier,minimumSpeed); }
    private Vector2 _frameAccel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _frameAccel = Vector2.zero;
        if (Input.GetKey(KeyCode.W)){
            _frameAccel.y += effectiveSpeed() * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _frameAccel.y -= effectiveSpeed() * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _frameAccel.x += effectiveSpeed() * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _frameAccel.x -= effectiveSpeed() * Time.deltaTime;
        }


        gameObject.transform.Translate(_frameAccel);
    }
}
