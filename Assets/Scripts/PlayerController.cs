using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.name);
        if (other.CompareTag("RoadTrigger")) {
            RoadManager.instance.GenerateNewRoad();
            other.GetComponent<RoadDestructor>().Destruct(1f);
        }
    }
}
