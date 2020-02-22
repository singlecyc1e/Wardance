using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Footstepaudio : MonoBehaviour
{
    // Start is called before the first frame update

    private AudioSource a;
    public AudioClip [] f;
    private void Awake()
    {
        a = this.GetComponent<AudioSource>();
        AudioSystem.instance.JumpAudio.AddListener(start_jump);
        AudioSystem.instance.onGroundAudio.AddListener(startground);
    }

    void startground() {
        a.loop = true;
        a.clip = f[0];
        a.Play();
    }
    void start_jump() {
        a.loop = false;
        a.clip = f[1];
        a.Play();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
