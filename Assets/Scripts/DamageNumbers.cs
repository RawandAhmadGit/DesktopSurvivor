using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumbers : MonoBehaviour
{
    public TMPro.TextMeshPro damageText; // Reference to the TextMeshPro component for displaying the damage
    public float displayDuration = 2f; // Duration for which the damage popup is displayed
    private float currentDuration = 0f; // Current duration of the damage popup

    public void ShowDamage(float damage)
    {
        // Set the damage text to the provided damage value
        damageText.text = damage.ToString();

        // Reset the current duration
        currentDuration = 0f;

        // Activate the damage popup GameObject
        gameObject.SetActive(true);
    }

    private void Update()
    {
        // Increase the current duration
        currentDuration += Time.deltaTime;

        // Check if the damage popup duration exceeds the display duration
        if (currentDuration >= displayDuration)
        {
            // Deactivate the damage popup GameObject
            gameObject.SetActive(false);
        }
    }
}


