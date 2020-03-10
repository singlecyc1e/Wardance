using UnityEngine;

public class CheckpointController : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            // Debug.Log("Save");
            RoadManager.instance.Save();
        }
    }
}
