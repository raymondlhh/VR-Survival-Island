using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaMap : MonoBehaviour
{
    public float frequency = 1f; // Frequency of the up and down movement
    public float amplitude = 1f; // Amplitude of the up and down movement
    private Vector3 startPosition; // Initial position of the GameObject

    // Start is called before the first frame update
    void Start()
    {
        // Save the initial position to start the oscillation from this point
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the new Y position using the sine of time
        float newY = startPosition.y + amplitude * Mathf.Sin(Time.time * frequency);

        // Update the position of the GameObject
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
