using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadDestructor : MonoBehaviour {
    public GameObject road;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("RoadDestructTrigger")) {
            Destroy(road);
        }
    }
}
