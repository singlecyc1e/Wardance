using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class EnemyMappingDictionary : SerializableDictionary<EnemyType, GameObject> { }

[Serializable]
public struct RoadInfo {
    public Transform spawnPoint;
    public Transform endPoint;
    public GameObject[] preBuild;
}

public class RoadManager : MonoBehaviour {
    public float speed;
//    public RoadInfo[] RoadInfos;
    public GameObject emptyRoad;

    public EnemyMappingDictionary enemyMapping;

    public static List<EnemyType> noEnemy;
    public static List<RoadSegmentInfo> roadInfo;

    private int roadIndex;
    
    public static RoadManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("Try to load two RoadManager.");
        }

        noEnemy = new List<EnemyType>() {EnemyType.None, EnemyType.None, EnemyType.None};
        roadInfo = DataUtility.GetLevelInfo(6);
        roadIndex = 0;
        InvokeRepeating(nameof(ShuffleIndex), 2f, 2f);
    }

    private void ShuffleIndex() {
        roadIndex = Random.Range(0, roadInfo.Count);
    }

    public RoadSegmentInfo GetRoadSegment() {
        return roadInfo[roadIndex];
    }

//    public void GenerateNewRoadSegment() {
//        foreach (var roadInfo in RoadInfos) {
//            var road = Instantiate(roads[Random.Range(0, roads.Length)], roadInfo.spawnPoint.position, Quaternion.identity);
//            road.GetComponent<RoadSegmentController>().Init(roadInfo.endPoint.position, speed);
//        }
//    }
}