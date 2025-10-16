using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EatingBehavior : MonoBehaviour
{
    private bool isBeingGrabbed = false; // Track grab state

    [SerializeField] public ParticleSystem eatingEffect;
    [SerializeField] public float destroyDelay = 3.0f;

    public bool isEating = false;

    // Start is called before the first frame update
    public void Start()
    {
        isEating = false;
    }

    // Update is called once per frame
    public void Update()
    {

    }

    public void Eating()
    {
        //eatingEffect.Play();
        StartCoroutine(StartEating());
        //();

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Mouth")
        {
            Eating();
        }


    }

    public IEnumerator StartEating()
    {
        eatingEffect.Play();

        yield return new WaitForSeconds(destroyDelay);

        Destroy(gameObject);
    }

    //public void Check()
    //{
    //    Debug.Log("Now checking...");

    //    if (isEating)
    //    {
    //        Debug.Log("Eating!");
    //    }
    //    if (!isEating)
    //    {
    //        Debug.Log("NOT Eating!");
    //    }
    //}
}
