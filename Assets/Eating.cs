using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating : MonoBehaviour
{
    public AudioSource eatSound;
    public AudioSource drinkSound;
    public float delayTime = 3f;

    public GameObject Object;

    public EnergyBarController energy;
    public ThirstBarController thirst;
    public LifeBarController life;

    public GameObject poisonEffect;
    

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
        

        if (other.gameObject.layer == LayerMask.NameToLayer("Food"))
        {
            energy.IncreaseEnergy();
            Debug.Log("Increase Energy");
            Object = other.gameObject;
            eatSound.Play();

            StartCoroutine(DelayDestroy(Object, 3f));  // Destroy all food after 3 seconds, including poisoned
            
            if (other.CompareTag("Poison"))
            {
                PoisonEffect();  // Start poison effects independently of the food destruction
                
            }
      
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Drink"))
        {
            thirst.IncreaseThirst();
            Debug.Log("Increase Water");
            Object = other.gameObject;
            drinkSound.Play();
            StartCoroutine(DelayDestroy(Object, 3f));
        }
    }

    private void PoisonEffect()
    {
        if (poisonEffect != null)
        {
            poisonEffect.SetActive(true);  // Activate the poison effect visually
            Debug.Log("Poison effect activated due to consuming poisonous food.");
        }
        life.DecreaseLife50();  // Call the DecreaseLife50() from LifeBarController to apply life reduction
    }

    

    IEnumerator DelayDestroy(GameObject obj, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(obj);
        poisonEffect.SetActive(false);
    }
}
