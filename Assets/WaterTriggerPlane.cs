using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTriggerPlane : MonoBehaviour
{
    public GameObject watersphere;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pot"))
        {
            watersphere.SetActive(true);
        }
    }
}
