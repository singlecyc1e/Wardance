using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadDestructor : MonoBehaviour {
    public GameObject road;
    
    public void Destruct(float t) {
        Destroy(road, t);
    }
}
