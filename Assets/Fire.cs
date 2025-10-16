using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
   //public FireWoodDone Done;

    [SerializeField] public ParticleSystem fireEffect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PutOnFire()
    {
        fireEffect.Play();
    }

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collide!");
        //if (Done.FireWood)
        //{
            //Debug.Log("FireWood is True");
            if (other.gameObject.layer == LayerMask.NameToLayer("Fire"))
            {
                PutOnFire();
                Debug.Log("PutOnFire");
            }
        //}
    }
}