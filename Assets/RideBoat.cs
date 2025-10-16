using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideBoat : MonoBehaviour
{
    public GameObject player;
    public GameObject boat;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dock"))
        {
            Debug.Log("TouchWater");
            float stepSize = 1f; // Distance to move forward each time
            Vector3 direction = new Vector3(1, 0, 1).normalized; // Diagonal direction (X and Z)

            // Calculate the new position by adding direction * stepSize
            Vector3 newPosition = player.transform.position + direction * stepSize;
            Vector3 newBPosition = boat.transform.position + direction * stepSize;

            // Update the player's position
            player.transform.position = newPosition;
            boat.transform.position = newBPosition;
        }
    }
}