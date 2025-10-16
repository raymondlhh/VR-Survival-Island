using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swim : MonoBehaviour
{
    public Animator animator;        // Reference to the Animator
    public float speed = 2f;         // Speed of the fish
    public float changeTime = 3f;    // Time to change direction
    private Vector3 targetDirection; // Current swim direction
    private float timer;

    void Start()
    {
        ChangeDirection(); // Set initial direction
    }

    void Update()
    {
        // Move in the current direction
        transform.Translate(targetDirection * speed * Time.deltaTime, Space.World);

        // Update Animator Parameters for direction
        animator.SetFloat("MoveX", targetDirection.x);
        animator.SetFloat("MoveZ", targetDirection.z);

        // Change direction after a timer
        timer += Time.deltaTime;
        if (timer >= changeTime)
        {
            ChangeDirection();
            timer = 0f;
        }

        // Rotate the fish to face the swim direction
        if (targetDirection != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
        }
    }

    void ChangeDirection()
    {
        // Randomize a direction in XZ plane
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        targetDirection = new Vector3(randomX, 0f, randomZ).normalized;
    }
}
