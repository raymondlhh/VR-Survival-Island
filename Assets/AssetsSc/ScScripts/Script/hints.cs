using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hints : MonoBehaviour
{
    public float amplitude;
    public float frequency;
    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        FloatObject();
    }
    void FloatObject()
    {
        // Calculate the new Y position using a sine wave
        float newY = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;

        // Apply the new position to the object
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
