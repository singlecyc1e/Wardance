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
    private AudioSource a;
    private void Awake()
    {
        instance = this;
        EnemydieAudio.AddListener(playenemydie);
    }
    void Start()
    {
        a = this.GetComponent<AudioSource>();
    }

    void playenemydie()
    {
        a.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
