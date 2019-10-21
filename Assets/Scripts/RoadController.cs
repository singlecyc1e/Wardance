using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoadController : MonoBehaviour {
    public RoadInfo roadInfo;

    private RoadManager roadManager;

    private void Start() {
        roadManager = RoadManager.instance;
        
        for (var i = 0; i < roadInfo.preBuild.Length; ++i) {
            if (i == roadInfo.preBuild.Length - 1) {
                roadInfo.preBuild[i].GetComponent<RoadSegmentController>().
                    Init(roadInfo.endPoint.position, roadManager.speed, this);
            } else {
                roadInfo.preBuild[i].GetComponent<RoadSegmentController>().
                    Init(roadInfo.endPoint.position, roadManager.speed, this, true);
            }
        }
    }
    
    public void GenerateNewRoadSegment() {
        var roads = roadManager.roads;
        
        var road = Instantiate(roads[Random.Range(0, roads.Length)], roadInfo.spawnPoint.position, Quaternion.identity, transform);
        road.GetComponent<RoadSegmentController>().Init(roadInfo.endPoint.position, roadManager.speed, this);
    }
}
