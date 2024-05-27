using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    [SerializeField] private float wheelRotation = 100f;
    [SerializeField] public bool rotateClockwise = true; // Dirección de rotación

    // Update is called once per frame
    void Update()
    {
        float rotationDirection = rotateClockwise ? 1f : -1f;
        transform.Rotate(new Vector3(0, 0, rotationDirection * wheelRotation * Time.deltaTime));
    }

    public void SetWheelRotation(float newRotation, float speedDifference)
    {
        wheelRotation = newRotation + speedDifference;
    }
}
