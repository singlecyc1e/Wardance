using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragesound : MonoBehaviour
{
    private AudioSource mysource;
    public AudioClip [] myclip;

    private void Awake()
    {
        mysource = this.GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        AudioSystem.instance.Rageon.AddListener(rageeffect);
        AudioSystem.instance.Rageoff.AddListener(rageoffeffect);
    }

    void rageeffect()
    {
        mysource.clip = myclip[0];
        mysource.Play();
    }

    void rageoffeffect()
    {
        mysource.clip = myclip[1];
        mysource.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
