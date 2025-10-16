using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunActions : MonoBehaviour
{
    public float Speed = 50;
    public GameObject bullet;
    public Transform Silencer;
    public AudioSource gunSound;
    public ParticleSystem smokeEffect; // Reference to the smoke particle system
    //public Transform smokePosition; // Reference to the empty GameObject for smoke positioning

    public void Fire()
    {
        GameObject spawnBullet = Instantiate(bullet, Silencer.position, Silencer.rotation);
        spawnBullet.GetComponent<Rigidbody>().velocity = Speed * Silencer.forward;
        gunSound.Play();

        
        //smokeEffect.transform.position = smokePosition.position;
        //smokeEffect.transform.rotation = smokePosition.rotation;
        smokeEffect.Play();

        Destroy(spawnBullet, 2);
    }
    // Start is called before the first frame update
    void Start()
    {
        smokeEffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
