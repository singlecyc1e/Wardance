using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDisplay : MonoBehaviour
{
    // level 1 28 sec
    // Start is called before the first frame update
    public Transform Endposition;
    public float level1 = 20f;
    private float overallSpeed = 0f;
    void Start()
    {
        Transform originalPosition = this.transform;
        overallSpeed = ((Endposition.position.x - originalPosition.position.x) * Time.deltaTime/ level1);
    }

    // Update is called once per frame
    void Update()
    {

        this.transform.position = new Vector3(this.transform.position.x + overallSpeed, this.transform.position.y, this.transform.position.z);
    }
}
