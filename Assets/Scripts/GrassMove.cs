using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassMove : MonoBehaviour
{
    private float speed;

    private void Start()
    {
        speed = RoadManager.instance.currentSpeed;
        this.transform.Rotate(Vector3.up * UnityEngine.Random.Range(-1000, 1000));
        this.transform.localPosition += new Vector3(UnityEngine.Random.Range(-1.5f, 1.5f), 0, UnityEngine.Random.Range(-.5f, .5f));
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), speed * Time.deltaTime);
    }
}
