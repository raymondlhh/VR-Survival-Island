using System.Collections;
using UnityEngine;

public class CanvaHint : MonoBehaviour
{
    public GameObject triggerMap; // Reference to the TriggerMap GameObject

    // Start is called before the first frame update
    void Start()
    {
        if (triggerMap == null)
        {
            // Find the TriggerMap GameObject in the scene if not assigned
            triggerMap = GameObject.Find("TriggerMap");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the TriggerMap GameObject is not active
        if (triggerMap != null && !triggerMap.activeSelf)
        {
            StartCoroutine(DeactivateAfterDelay());
        }
    }

    IEnumerator DeactivateAfterDelay()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(3);

        // Set this GameObject to inactive
        gameObject.SetActive(false);
    }
}
