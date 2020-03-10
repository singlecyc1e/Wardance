using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

[Serializable]
public class RoadIndexChangeEvent : UnityEvent<int> {
}

public class RoadManager : MonoBehaviour {
    public float speed;
    public GameObject emptyRoad;
    public EnemyMappingDictionary enemyMapping;

    public float speedReduceDuration;
    public float speedReduceDelay;
    public int minimumSwipeCount;

    public bool isTutorial;
    
    public RoadIndexChangeEvent onRoadIndexChange;

    private int nextCheckpoint;
    [NonSerialized] public int savedOffset;
    [NonSerialized] public float currentSpeed;

    private bool randomRoadSpawn;
    private static List<RoadSegmentInfo> roadInfo;
    private int roadIndex;

    private bool willSpawnBoss;

    public const string ROAD_SAVE_OFFSET = "ROAD_SAVE_OFFSET";
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

        nextCheckpoint = -1;
        
        if (!PlayerPrefs.HasKey(ROAD_SAVE_OFFSET)) {
            Save(0);
        }
        // Save(0);  // For debug only
        savedOffset = PlayerPrefs.GetInt(ROAD_SAVE_OFFSET);
    }

    public void Init() {
        roadInfo = DataUtility.GetLevelInfo(false, isTutorial);
        randomRoadSpawn = false;
        roadIndex = 0;
        // if (level >= 6) {
        //     randomRoadSpawn = true;
        // } else {
        //     randomRoadSpawn = false;
        //     roadIndex = 0;
        // }
    }

    public void InitProgressBar(ProgressBarController progressBarController) {
        onRoadIndexChange.AddListener(progressBarController.Progress);
    }

    public int GetRoadCount() {
        return roadInfo.Count;
    }

    public void Save(int manualOffset = -1) {
        if (manualOffset != -1) {
            PlayerPrefs.SetInt(ROAD_SAVE_OFFSET, manualOffset);
            return;
        }
        
        if(nextCheckpoint == -1) return;
        
        PlayerPrefs.SetInt(ROAD_SAVE_OFFSET, nextCheckpoint);
    }

    private void ChangeRoadIndex(int currentIndex = -1) {
        if (randomRoadSpawn) {
            roadIndex = Random.Range(0, roadInfo.Count);
        } else {
            roadIndex = currentIndex + 1;
        }
        onRoadIndexChange.Invoke(currentIndex);
    }

    public RoadSegmentInfo GetRoadSegment(int currentIndex) {
        if (roadIndex >= roadInfo.Count) {
            roadInfo = DataUtility.GetLevelInfo(true);
            roadIndex = 0;
            InvokeRepeating(nameof(ChangeRoadIndex), 0f, 1.5f);
        }

        ChangeRoadIndex(currentIndex);
        var result = roadInfo[roadIndex];

        if (result.GetEnemyTypesAt(1)[1] == EnemyType.Boss && !willSpawnBoss) {
            willSpawnBoss = true;
            ++roadIndex;
            StartCoroutine(ReduceSpeed(speedReduceDelay));
        } else if (result.GetEnemyTypesAt(1)[1] == EnemyType.Save) {
            nextCheckpoint = currentIndex;
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