using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDeathandbegin : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource mysource;
    public AudioClip deathsound;
    public AudioClip startsound;
    private void Awake()
    {
        mysource = this.GetComponent<AudioSource>();

    }

    void death()
    {
        mysource.clip = deathsound;
        mysource.Play();
    }
    void Start()
    {
        AudioSystem.instance.DeathAudio.AddListener(death);
        mysource.clip = startsound;
        mysource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
