using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resizer : MonoBehaviour {
    private PlayerAttack playerAttack;
    private Vector3 speed;
    // Start is called before the first frame update
    void Start() {
        playerAttack = GetComponent<PlayerAttack>();
        float direction = (float)playerAttack.burstPjtlNr / (float)playerAttack.burstTotalPjtl;
        direction *= 2 * Mathf.PI;
        speed.x = Mathf.Cos(direction);
        speed.y = Mathf.Sin(direction);
        transform.localScale = playerAttack.wData.size1 * Vector3.one;
        speed *= playerAttack.wData.projectileSpeed;
        speed *= playerAttack.player.projectilespeedModifier;
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(speed * Time.deltaTime);
    }
}
