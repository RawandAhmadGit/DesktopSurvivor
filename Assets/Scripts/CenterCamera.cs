using UnityEngine;

public class CenterCamera : MonoBehaviour {
    public Transform player;
    public float smoothSpeed = 0.5f;

    private Vector3 offset;

    private void Start() {
        // Calculate the initial offset between the camera and the player
        offset = transform.position - player.position;
    }

    private void LateUpdate() {
        // Calculate the desired position of the camera
        Vector3 desiredPosition = player.position + offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;
    }
}
