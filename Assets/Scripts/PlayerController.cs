using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float duration;

    private bool moving;
    private float startTime;
    private float targetZ;
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("RoadTrigger")) {
            RoadManager.instance.GenerateNewRoadSegment();
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            if(moving) return;

            moving = true;
            startTime = Time.time;
            targetZ = transform.position.z + 2.5f;
        } else if (Input.GetKeyDown(KeyCode.D)) {
            if(moving) return;

            moving = true;
            startTime = Time.time;
            targetZ = transform.position.z - 2.5f;
        }
        
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
