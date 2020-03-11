using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioSystem : MonoBehaviour
{
    static public AudioSystem instance;
    // Start is called before the first frame update
    public UnityEvent onGroundAudio;
    public UnityEvent JumpAudio;
    public UnityEvent EnemydieAudio;
    public UnityEvent DeathAudio;
    public UnityEvent Rageon;
    public UnityEvent Rageoff;
    public AudioClip [] x; 
    public UnityEvent rockdie;

    public UnityEvent wooddie;
    private AudioSource a;
    private void Awake()
    {
        instance = this;
        EnemydieAudio.AddListener(playenemydie);
        wooddie.AddListener(woodenemydie);
        rockdie.AddListener(rockenemydie);
    }
    void Start()
    {
        a = this.GetComponent<AudioSource>();
    }

    void playenemydie()
    {
        a.clip = x[0];
        a.Play();
    }

    void woodenemydie()
    {
        a.clip = x[1];
        a.Play();
    }

    void rockenemydie()
    {
        a.clip = x[2];
        a.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
