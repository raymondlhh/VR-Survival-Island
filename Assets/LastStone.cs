using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastStone : MonoBehaviour
{
    public GameObject hoverIndicator;
    public GameObject originalObject;

    public bool isReplaced;

    public VideoController videoController; // Reference to the VideoController

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (!isReplaced)
        {  
                isReplaced = true;
            Debug.Log("CutScene3!");
                videoController.PlayVideo(2);
        }
    }
}
