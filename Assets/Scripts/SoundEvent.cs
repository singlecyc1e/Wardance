using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundEvent a;
    public UnityEvent slash;
    public AudioClip[] audiolist;
    public AudioSource ads;
    private void Awake()
    {
        a = this;
        ads = this.GetComponent<AudioSource>();
        slash.AddListener(slash_sound);
    }
    void Start()
    {
        
    }

    public void slash_sound()
    {
        ads.clip = audiolist[Random.Range(0, audiolist.Length)];
        ads.Play();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
