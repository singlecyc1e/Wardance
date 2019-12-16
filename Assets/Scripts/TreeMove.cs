using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMove : MonoBehaviour
{
    public float speed = 8f;

    void Update() {
        speed = RoadManager.instance.currentSpeed;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x-1 , transform.position.y, transform.position.z ), speed * Time.deltaTime);
    }
}
