using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingIcon : MonoBehaviour
{
    private Vector3 finalSize;
    PlayerAttack playerAttack;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        finalSize = Vector3.one * playerAttack.wData.size1;
        transform.localScale = Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        float relativeLifetime = playerAttack.currentDuration / playerAttack.totalDuration;
        transform.localScale = Vector3.Lerp(Vector3.one, finalSize, relativeLifetime);
        transform.Rotate(0, 0, (360 / playerAttack.wData.maxDuration) * Time.deltaTime);
        Color newColor = new(1, 1, 1, 1 - (relativeLifetime * relativeLifetime));
        spriteRenderer.color = newColor;
    }
}
