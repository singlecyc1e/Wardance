using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgm : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource[] s;
    void Start()
    {
        s = this.GetComponentsInChildren<AudioSource>();
        AudioSystem.instance.Rageoff.AddListener(rageoff);
        AudioSystem.instance.Rageon.AddListener(rageon);
    }

    private void rageoff()
    {
        s[0].volume = 1;
        s[1].volume = 0;
    }

    private void rageon()
    {
        s[1].volume = 1;
        s[0].volume = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
