using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicContainer : MonoBehaviour
{
    public AudioMixer mixer;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
