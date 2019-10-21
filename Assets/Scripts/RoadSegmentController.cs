using System;
using UnityEngine;

public class RoadSegmentController : MonoBehaviour {

    private float speed;
    private Vector3 target;
    private bool initialized;
    private bool prebuild;
    private RoadController roadController;

    public void Init(Vector3 newTarget, float newSpeed, RoadController newRoadController, bool isPrebuild = false) {
        roadController = newRoadController;
        target = newTarget;
        speed = newSpeed;
        prebuild = isPrebuild;
        initialized = true;
    }
    
    private void Update() {
        if(!initialized) return;
        
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (!prebuild && other.CompareTag("LevelArea")) {
            roadController.GenerateNewRoadSegment();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("LevelArea")) {
            Destroy(gameObject);
        }
    }
}
