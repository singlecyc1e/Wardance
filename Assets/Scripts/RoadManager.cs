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
    
    public float speedReduceDuration;
    public float speedReduceDelay;
    public int minimumSwipeCount;
    
    [NonSerialized]
    public float currentSpeed;

    private bool randomRoadSpawn;
    private static List<RoadSegmentInfo> roadInfo;
    private int roadIndex;

    private bool willSpawnBoss;
    
    public static List<EnemyType> noEnemy;
    public static RoadManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("Try to load two RoadManager.");
        }

        noEnemy = new List<EnemyType>() {EnemyType.None, EnemyType.None, EnemyType.None};

        currentSpeed = speed;
        willSpawnBoss = false;
    }

    public void Init(int level) {
        roadInfo = DataUtility.GetLevelInfo(level);
        if (level >= 6) {
            randomRoadSpawn = true;
        } else {
            randomRoadSpawn = false;
            roadIndex = 0;
        }
        InvokeRepeating(nameof(ChangeRoadIndex), 0f, 2f);
    }

    private void ChangeRoadIndex() {
        if (randomRoadSpawn) {
            roadIndex = Random.Range(0, roadInfo.Count);
        } else {
            ++roadIndex;
        }
    }

    public RoadSegmentInfo GetRoadSegment() {
        var result = roadInfo[roadIndex];

        if (result.GetEnemyTypesAt(1)[1] == EnemyType.Boss && !willSpawnBoss) {
            willSpawnBoss = true;
            StartCoroutine(ReduceSpeed(speedReduceDelay));
        }
//        TimeController.instance.BulletTime();
//        Invoke(nameof(TimeController.instance.BulletTime), speedReduceDelay);
        return result;
    }

    private IEnumerator ReduceSpeed(float delay) {
        yield return new WaitForSeconds(delay);
        var fadeSpeed = currentSpeed / speedReduceDuration;

        while (!Mathf.Approximately (currentSpeed, 0f))
        {
            currentSpeed = Mathf.MoveTowards (currentSpeed, 0f, fadeSpeed * Time.deltaTime);
//            AudioListener.volume = Mathf.MoveTowards (AudioListener.volume, (PlayerPrefs.GetFloat ("volume") - finalAlpha), audioFadeSpeed * Time.deltaTime);
//			Debug.Log (faderCanvasGroup.alpha);

            yield return null;
        }
        
        LevelController.instance.StartSwipeCounting();
        yield return StartCoroutine(WaitForBossBattle());
    }

    private IEnumerator WaitForBossBattle() {
        yield return new WaitForSecondsRealtime(3f);

        var swipes = LevelController.instance.GetSwipeResultAndClear();
        if (swipes < minimumSwipeCount) {
            WeaponDMG.instance.SetupDeathMenu();
        } else {
            yield return StartCoroutine(ContinueGame());
        }
    }

    private IEnumerator ContinueGame() {
        var fadeSpeed = speed / speedReduceDuration;
        while (!Mathf.Approximately (currentSpeed, speed))
        {
            currentSpeed = Mathf.MoveTowards (currentSpeed, speed, fadeSpeed * Time.deltaTime);
            yield return null;
        }

        willSpawnBoss = false;
    }

//    public void GenerateNewRoadSegment() {
//        foreach (var roadInfo in RoadInfos) {
//            var road = Instantiate(roads[Random.Range(0, roads.Length)], roadInfo.spawnPoint.position, Quaternion.identity);
//            road.GetComponent<RoadSegmentController>().Init(roadInfo.endPoint.position, currentSpeed);
//        }
//    }
}