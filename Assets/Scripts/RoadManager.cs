using System;
using System.Collections;
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
    public GameObject emptyRoad;
    public EnemyMappingDictionary enemyMapping;
    public bool willSpawnBoss;
    public float speedReduceDuration;
    public float speedReduceDelay;

    public static List<EnemyType> noEnemy;
    
    private static List<RoadSegmentInfo> roadInfo;
    private int roadIndex;
    
    public static RoadManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("Try to load two RoadManager.");
        }

        noEnemy = new List<EnemyType>() {EnemyType.None, EnemyType.None, EnemyType.None};
        roadInfo = DataUtility.GetLevelInfo(1);
        roadIndex = 0;
        InvokeRepeating(nameof(ShuffleIndex), 2f, 2f);
    }

    private void ShuffleIndex() {
        roadIndex = Random.Range(0, roadInfo.Count);
    }

    public RoadSegmentInfo GetRoadSegment() {
        var result = roadInfo[roadIndex];
        if (result.GetEnemyTypesAt(1)[1] == EnemyType.Boss) {
            StartCoroutine(ReduceSpeed(speedReduceDelay));
        }
        return roadInfo[roadIndex];
    }

    public IEnumerator ReduceSpeed(float delay) {
        yield return new WaitForSeconds(delay);
        float fadeSpeed = speed / speedReduceDuration;
//        float audioFadeSpeed = Mathf.Abs (AudioListener.volume - (PlayerPrefs.GetFloat ("volume") - finalAlpha)) / fadeDuration;

        while (!Mathf.Approximately (speed, 0f))
        {
            speed = Mathf.MoveTowards (speed, 0f, fadeSpeed * Time.deltaTime);
//            AudioListener.volume = Mathf.MoveTowards (AudioListener.volume, (PlayerPrefs.GetFloat ("volume") - finalAlpha), audioFadeSpeed * Time.deltaTime);
//			Debug.Log (faderCanvasGroup.alpha);

            yield return null;
        }
//		Debug.Log ("fade end" + Time.time);

//        isFading = false;
    }

//    public void GenerateNewRoadSegment() {
//        foreach (var roadInfo in RoadInfos) {
//            var road = Instantiate(roads[Random.Range(0, roads.Length)], roadInfo.spawnPoint.position, Quaternion.identity);
//            road.GetComponent<RoadSegmentController>().Init(roadInfo.endPoint.position, speed);
//        }
//    }
}