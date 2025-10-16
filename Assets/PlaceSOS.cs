using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceSOS : MonoBehaviour
{
    public GameObject hoverIndicator;
    public GameObject replacementObject;
    public GameObject originalObject;
    public GameObject parentObject;
    public GameObject objectToActivate;

    public Transform locationToPlace;

    public SOS_StoneUI Stone;

    public bool isReplaced;

    public VideoController videoController; // Reference to the VideoController
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Store Location");
        isReplaced = false;
        if (locationToPlace == null)
        {
            Debug.LogError("locationToPlace is not assigned! Assign it in the Inspector.");
            return;
        }
        locationToPlace.position = parentObject.transform.position;
        locationToPlace.rotation = parentObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Hover()
    {
        hoverIndicator.SetActive(true);
        originalObject.SetActive(false);


    }

    public void ExitHover()
    {
        originalObject.SetActive(true);
        hoverIndicator.SetActive(false);

    }

    public void PutStone()
    {
        Debug.Log("Number: " + Stone.stoneCount);
        if (!isReplaced && Stone.stoneCount > 0)
        {
            objectToActivate.SetActive(true);
            Destroy(parentObject);
            Instantiate(replacementObject, locationToPlace.position, locationToPlace.rotation);
            isReplaced = true;
            Stone.stoneCount--;
        }
    }
}
