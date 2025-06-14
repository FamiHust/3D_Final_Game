// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class DoNotDestroy : MonoBehaviour
// {
//     private void Awake() 
//     {
//         GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");

//         if (musicObj.Length > 1)
//         {
//             Destroy(this.gameObject);
//         }    

//         DontDestroyOnLoad(this.gameObject);
//     }
// }
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DoNotDestroy : MonoBehaviour
{
    public static DoNotDestroy instance;
    private AudioSource musicSource;

    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");

        if (musicObj.Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        instance = this;
        musicSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        musicSource.volume = savedVolume;
    }

    public void SetVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public float GetVolume()
    {
        return musicSource.volume;
    }
}
