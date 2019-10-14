using UnityEngine;

public class RoadManager : MonoBehaviour {
    public float speed;
    public GameObject spawnPoint;
    public Transform endPoint;
    public GameObject[] roads;

    public static RoadManager instance;

    private void Start() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("Try to load two RoadManager.");
        }
    }

    public void GenerateNewRoad() {
        Instantiate(roads[Random.Range(0, roads.Length)], spawnPoint.transform.position, Quaternion.identity);
    }
}