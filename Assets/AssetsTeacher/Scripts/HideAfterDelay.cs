using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAfterDelay : MonoBehaviour
{
    public float delayInSecond = 5f;
    public float fadeRate = 0.25f;

    private CanvasGroup canvaGroup;
    private float startTimer;
    private float fadeoutTimer;

    private void OnEnable()
    {
        canvaGroup = GetComponent<CanvasGroup>();
        canvaGroup.alpha = 1f;

        startTimer = Time.time + delayInSecond;
        fadeoutTimer = fadeRate;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= startTimer)
        {
            fadeoutTimer -= Time.deltaTime;

            //fade out complete?
            if (fadeoutTimer <= 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                //reduce the alpha value
                canvaGroup.alpha = fadeoutTimer / fadeRate;
            }
        }
    }
}
