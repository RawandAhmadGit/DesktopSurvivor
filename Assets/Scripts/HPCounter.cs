using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPCounter : MonoBehaviour
{
    public playerCollision playerCollision; // reference to the playerCollision script
    private Text text; // reference to the text component

    private void Start()
    {
        text = GetComponent<Text>(); // get the text component
    }

    private void Update()
    {
        text.text = "HP: " + playerCollision.currentHealth.ToString() + "/" + playerCollision.maxHealth.ToString(); // set the text to display the current and max health of the player
    }
}
