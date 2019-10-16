using UnityEngine;

public class RoadController : MonoBehaviour {

    private float speed;
    private Vector3 target;
    private bool initialized;

    public void Init(Vector3 newTarget, float newSpeed) {
        target = newTarget;
        speed = newSpeed;
        initialized = true;
    }
    
    private void Update() {
        if(!initialized) return;
        
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
