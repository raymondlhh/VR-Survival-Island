using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDoorAnimation : MonoBehaviour
{
    Animator anim;
    public AudioSource openSound;
    public AudioSource closeSound;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAnimation(bool newValue)
    {
        if(newValue)
        {
            anim.Play("doorOpen");
            openSound.Play();
        }
        else
        {
            anim.Play("doorClose");
            closeSound.PlayDelayed(1.3f);
        }
    }
}
