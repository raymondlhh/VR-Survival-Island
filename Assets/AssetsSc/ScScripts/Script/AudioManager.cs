using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioSource")]
    [SerializeField] AudioSource BGMMain;  // Main BGM AudioSource
    [SerializeField] AudioSource BGMAmbient;  // Ambient BGM AudioSource
    [SerializeField] AudioSource SFX;  // Sound Effects AudioSource

    [Header("AudioClip")]
    public AudioClip MainBGM;
    public AudioClip AmbientBGM;
    public AudioClip hurt;
    public AudioClip pickitem;
    public AudioClip footstep;
    public AudioClip drinkwater;
    public AudioClip thristy;
    public AudioClip eat;
    public AudioClip explosion;

    public void PlayBGM()
    {
        Debug.Log("Playing BGM...");

        // Set clips to the respective AudioSources
        BGMMain.clip = MainBGM;
        BGMAmbient.clip = AmbientBGM;

        // Play both BGM tracks simultaneously
        BGMMain.Play();
        BGMAmbient.Play();
    }


    public void PlaySFX(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }
}
