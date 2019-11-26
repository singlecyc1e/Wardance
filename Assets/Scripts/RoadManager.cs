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

    public int minimumSwipeCount;

    public static List<EnemyType> noEnemy;
    [NonSerialized]
    public float currentSpeed;
    
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
        currentSpeed = speed;
    }

    private void ShuffleIndex() {
        roadIndex = Random.Range(0, roadInfo.Count);
    }

    public RoadSegmentInfo GetRoadSegment() {
        var result = roadInfo[roadIndex];
        if (result.GetEnemyTypesAt(1)[1] == EnemyType.Boss) {
            StartCoroutine(ReduceSpeed(speedReduceDelay));
        }
//        TimeController.instance.BulletTime();
//        Invoke(nameof(TimeController.instance.BulletTime), speedReduceDelay);
        return result;
    }

    public IEnumerator ReduceSpeed(float delay) {
        yield return new WaitForSeconds(delay);
        float fadeSpeed = currentSpeed / speedReduceDuration;

        while (!Mathf.Approximately (currentSpeed, 0f))
        {
            currentSpeed = Mathf.MoveTowards (currentSpeed, 0f, fadeSpeed * Time.deltaTime);
//            AudioListener.volume = Mathf.MoveTowards (AudioListener.volume, (PlayerPrefs.GetFloat ("volume") - finalAlpha), audioFadeSpeed * Time.deltaTime);
//			Debug.Log (faderCanvasGroup.alpha);

            yield return null;
        }
        
        LevelController.instance.StartSwipeCounting();
        yield return StartCoroutine(ContinueGame());
    }

    public IEnumerator ContinueGame() {
        yield return new WaitForSecondsRealtime(3f);

        var swipes = LevelController.instance.GetSwipeResultAndClear();
        if (swipes < minimumSwipeCount) {
            WeaponDMG.instance.SetupDeathMenu();
            yield break;
        }
        
        float fadeSpeed = currentSpeed / speedReduceDuration;
        while (!Mathf.Approximately (currentSpeed, speed))
        {
            currentSpeed = Mathf.MoveTowards (currentSpeed, speed, fadeSpeed * Time.deltaTime);
            yield return null;
        }
    }

//    public void GenerateNewRoadSegment() {
//        foreach (var roadInfo in RoadInfos) {
//            var road = Instantiate(roads[Random.Range(0, roads.Length)], roadInfo.spawnPoint.position, Quaternion.identity);
//            road.GetComponent<RoadSegmentController>().Init(roadInfo.endPoint.position, currentSpeed);
//        }
//    }
}