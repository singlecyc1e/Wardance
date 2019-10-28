using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class RoadSegmentController : MonoBehaviour {
    public Transform[] spawnPoints;

    private float speed;
    private Vector3 target;
    private bool initialized;
    private bool prebuild;
    private RoadController roadController;

    public void Init(Vector3 newTarget, float newSpeed, List<EnemyType> enemy, RoadController newRoadController, bool isPrebuild = false) {
        roadController = newRoadController;
        target = newTarget;
        speed = newSpeed;
        prebuild = isPrebuild;
        
        SpawnEnemy(enemy);
        
        initialized = true;
    }

    private void SpawnEnemy(List<EnemyType> enemy) {
        if (spawnPoints.Length != enemy.Count) {
            
            return;
        }

        for (int i = 0; i < spawnPoints.Length; i++) {
            var enemyObject = RoadManager.instance.enemyMapping[enemy[i]];
            Instantiate(enemyObject, spawnPoints[i].position, Quaternion.identity, transform);
        }
    }
    
    private void Update() {
        if(!initialized) return;
        
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (!prebuild && other.CompareTag("LevelArea")) {
            roadController.GenerateNewRoadSegment();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("LevelArea")) {
            Destroy(gameObject,1);
        }
    }
}
