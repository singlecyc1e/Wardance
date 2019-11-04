using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float duration;

    public bool moving;
    private float startTime;
    private float targetZ;
    private Animator AnimeC;
    public float distance = 4f;
    public static PlayerController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        AnimeC = GameObject.Find("Sword").GetComponent<Animator>();
    }


    private void Update() {
        if (Input.GetKeyDown(KeyCode.A) && transform.position.z < distance) {
            if (moving) return;
            AnimeC.ResetTrigger("LS");
            AnimeC.ResetTrigger("RS");
            AnimeC.SetTrigger("LS");

            moving = true;
            startTime = Time.time;
            targetZ = transform.position.z + distance;
        } else if (Input.GetKeyDown(KeyCode.D) && transform.position.z > -distance) {
            if (moving) return;
            AnimeC.ResetTrigger("RS");
            AnimeC.ResetTrigger("LS");
            AnimeC.SetTrigger("RS");

            moving = true;
            startTime = Time.time;
            targetZ = transform.position.z - distance;
        }
    }

    private void FixedUpdate()
    {
   
        if(!moving) return;
        
        var t = (Time.time - startTime) / duration;
        var position = transform.position;
        var newZ = Mathf.SmoothStep(position.z, targetZ, t);
        position = new Vector3(position.x, position.y, newZ);
        transform.position = position;

        if (Mathf.Approximately(transform.position.z, targetZ)) {
            transform.position = new Vector3(position.x, position.y, targetZ);
            moving = false;
        }
    }


}
