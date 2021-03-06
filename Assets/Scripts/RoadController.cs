﻿using UnityEngine;

public class RoadController : MonoBehaviour {
    public RoadInfo roadInfo;
    public int roadNum;

    private int roadIndex;
    private RoadManager roadManager;

    private void Start() {
        roadManager = RoadManager.instance;
        roadIndex = RoadManager.instance.savedOffset;

        for (var i = 0; i < roadInfo.preBuild.Length; ++i) {
            if (i == roadInfo.preBuild.Length - 1) {
                roadInfo.preBuild[i].GetComponent<RoadSegmentController>().Init(roadInfo.endPoint.position,
                    roadManager.currentSpeed, RoadManager.noEnemy, this);
            } else {
                roadInfo.preBuild[i].GetComponent<RoadSegmentController>().Init(roadInfo.endPoint.position,
                    roadManager.currentSpeed, RoadManager.noEnemy, this, true);
            }
        }
    }

    public void GenerateNewRoadSegment() {
        var road = Instantiate(roadManager.emptyRoad, roadInfo.spawnPoint.position, Quaternion.identity, transform);
        road.GetComponent<RoadSegmentController>().Init
        (roadInfo.endPoint.position,
            roadManager.currentSpeed,
            RoadManager.instance.GetRoadSegment(roadIndex).GetEnemyTypesAt(roadNum),
            this);
        ++roadIndex;
    }
}