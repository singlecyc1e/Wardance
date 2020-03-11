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

    public UnityEvent wooddie;

    public UnityEvent rockdie;
    public UnityEvent Rageon;
    public UnityEvent Rageoff;
    private AudioSource a;

    public AudioClip [] x;
    private void Awake()
    {
        instance = this;
        EnemydieAudio.AddListener(playenemydie);
        wooddie.AddListener(playwooddie);
        rockdie.AddListener(playrockdie);
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

    void playrockdie()
    {
        a.clip = x[1];
        a.Play();
    }

    void playwooddie()
    {
        a.clip = x[2];
        a.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
