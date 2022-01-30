using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicContainer : MonoBehaviour
{
    public AudioMixer mixer;

    public AudioClip introClip;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Music").Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        var musicVol = PlayerPrefs.GetFloat("Music", .5f);
        mixer.SetFloat("Music", Mathf.Log10(musicVol) * 20 );

        var source = GetComponent<AudioSource>();
        source.PlayOneShot(introClip);
        source.PlayDelayed(introClip.length);
    }

}
