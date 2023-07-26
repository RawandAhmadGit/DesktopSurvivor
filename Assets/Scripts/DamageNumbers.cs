using UnityEngine;
using TMPro;
using System.Collections;

public class DamageNumbers : MonoBehaviour {
    public TextMeshPro damageText; // Reference to the TextMeshPro component for displaying the damage
    public float displayDuration = 1f; // Duration for which the damage popup is displayed

    private void Start() {
        // Deactivate the damage popup GameObject on start
        gameObject.SetActive(true);
    }

    public void ShowDamage(float damage) {
        // Set the damage text to the provided damage value
        damageText.text = damage.ToString();

        // Activate the damage popup GameObject
        gameObject.SetActive(true);

        // Start a coroutine to deactivate the damage popup after the display duration
        StartCoroutine(HideDamage());
    }

    private IEnumerator HideDamage() {
        // Wait for the display duration
        yield return new WaitForSeconds(displayDuration);

        // Deactivate the damage popup GameObject
        gameObject.SetActive(false);
    }
}
