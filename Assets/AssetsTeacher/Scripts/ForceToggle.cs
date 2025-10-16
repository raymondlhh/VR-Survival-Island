using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceToggle : MonoBehaviour
{
    private Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>(); 
    }

    public void Toggle()
    {
        toggle.isOn = !toggle.isOn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
