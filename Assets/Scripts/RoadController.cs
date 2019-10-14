using UnityEngine;

public class RoadController : MonoBehaviour {
        
    private void Update() {
        transform.position = Vector3.MoveTowards(transform.position, 
            RoadManager.instance.endPoint.position, 
            RoadManager.instance.speed * Time.deltaTime);
    }
}
