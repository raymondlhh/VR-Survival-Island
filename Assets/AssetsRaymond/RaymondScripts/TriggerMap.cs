using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMap : MonoBehaviour
{
    public GameObject woodSnap; // Reference to the WoodSnap GameObject
    public GameObject canvaHint; // Reference to the CanvaHint GameObject

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the other GameObjects are set active or inactive as needed initially
        if (woodSnap != null) woodSnap.SetActive(false);
        if (canvaHint != null) canvaHint.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Deactivate this GameObject
        gameObject.SetActive(false);

        // Activate the WoodSnap and CanvaHint GameObjects
        if (woodSnap != null) woodSnap.SetActive(true);
        if (canvaHint != null) canvaHint.SetActive(true);
    }
}
