using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct RoadInfo {
    public Transform spawnPoint;
    public Transform endPoint;
    public GameObject[] preBuild;
}

public class RoadManager : MonoBehaviour {
    public float speed;
//    public RoadInfo[] RoadInfos;
    public GameObject[] roads;

    public static RoadManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("Try to load two RoadManager.");
        }

//        foreach (var roadInfo in RoadInfos) {
//            foreach (var road in roadInfo.preBuild) {
//                road.GetComponent<RoadSegmentController>().Init(roadInfo.endPoint.position, speed);
//            }
//        }
    }

//    public void GenerateNewRoadSegment() {
//        foreach (var roadInfo in RoadInfos) {
//            var road = Instantiate(roads[Random.Range(0, roads.Length)], roadInfo.spawnPoint.position, Quaternion.identity);
//            road.GetComponent<RoadSegmentController>().Init(roadInfo.endPoint.position, speed);
//        }
//    }
}