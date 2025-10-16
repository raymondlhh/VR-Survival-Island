using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomCube : MonoBehaviour
{
    public GameObject cubePrefab; // Assign this in the inspector with your cube prefab
    public Vector3 spawnPosition = new Vector3(0, 0, 0); // Adjust this as needed

    // Public method to spawn a cube
    public void SpawnCube()
    {
        if (cubePrefab != null)
        {
            // Create an instance of the cube
            GameObject cube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);

            // Randomize the size
            float size = Random.Range(0.01f, 0.5f); // Random size between 0.5 and 2
            cube.transform.localScale = new Vector3(size, size, size);

            // Randomize the color
            Color randomColor = new Color(Random.value, Random.value, Random.value);
            cube.GetComponent<Renderer>().material.color = randomColor;

            // Destroy the cube after 1 second
            Destroy(cube, 1.0f);
        }
        else
        {
            Debug.LogError("Cube prefab is not assigned.");
        }
    }
}
