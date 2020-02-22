using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class EnemyMappingDictionary : SerializableDictionary<EnemyType, GameObject> {
}

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

    [NonSerialized] public float currentSpeed;

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
        roadInfo = DataUtility.GetLevelInfo();
        if (level >= 6) {
            randomRoadSpawn = true;
        } else {
            randomRoadSpawn = false;
            roadIndex = 0;
        }
    }

    private void ChangeRoadIndex(int currentIndex = -1) {
        if (randomRoadSpawn) {
            roadIndex = Random.Range(0, roadInfo.Count);
        } else {
            roadIndex = currentIndex + 1;
        }
    }

    public RoadSegmentInfo GetRoadSegment(int currentIndex) {
        if (roadIndex >= roadInfo.Count) {
            roadInfo = DataUtility.GetLevelInfo(6);
            roadIndex = 0;
            InvokeRepeating(nameof(ChangeRoadIndex), 0f, 1.5f);
        }

        ChangeRoadIndex(currentIndex);
        var result = roadInfo[roadIndex];

        if (result.GetEnemyTypesAt(1)[1] == EnemyType.Boss && !willSpawnBoss) {
            willSpawnBoss = true;
            ++roadIndex;
            StartCoroutine(ReduceSpeed(speedReduceDelay));
        }

        return result;
    }

    private IEnumerator ReduceSpeed(float delay) {
        yield return new WaitForSeconds(delay);
        var fadeSpeed = currentSpeed / speedReduceDuration;

        while (!Mathf.Approximately(currentSpeed, 0f)) {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, fadeSpeed * Time.deltaTime);
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
        while (!Mathf.Approximately(currentSpeed, speed)) {
            currentSpeed = Mathf.MoveTowards(currentSpeed, speed, fadeSpeed * Time.deltaTime);
            yield return null;
        }

        willSpawnBoss = false;
    }
}