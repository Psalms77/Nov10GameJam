using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBackground : MonoBehaviour
{
    public float rotationSpeed;

    void Update()
    {
        // Get the input for horizontal movement (A and D keys or left and right arrow keys)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Rotate the object based on the input
        transform.Rotate(Vector3.forward, horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
