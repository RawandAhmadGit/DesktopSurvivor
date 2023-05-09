using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genericEnemyScript : MonoBehaviour
{
    public playerScript thePlayer;
    public float speed = 1f;
    
    genericEnemyScript(playerScript thePlayer, Vector2 pos)
    {
        this.thePlayer = thePlayer;
        gameObject.transform.position = pos;
    }


    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<playerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = thePlayer.GetComponent<Transform>().position;
        Vector3 connectionLine = target - gameObject.transform.position;
        connectionLine.Normalize();
        connectionLine.Scale(new Vector3(Time.deltaTime * speed, Time.deltaTime * speed, Time.deltaTime * speed));
        gameObject.transform.Translate(connectionLine);
    }
}
