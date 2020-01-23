using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMove : MonoBehaviour
{
    public float speed = 8f;
    private void Start()
    {
        speed = RoadManager.instance.currentSpeed;
        Transform tree1 = transform.GetChild(0);
        Transform tree2 = transform.GetChild(1);
        Transform tree3 = transform.GetChild(2);
        Transform tree4 = transform.GetChild(3);
        Transform tree5 = transform.GetChild(4);
        Transform tree6 = transform.GetChild(5);
        tree1.transform.Rotate(Vector3.up * UnityEngine.Random.Range(-1000,1000));
        tree2.transform.Rotate(Vector3.up * UnityEngine.Random.Range(-1000, 1000));
        tree3.transform.Rotate(Vector3.up * UnityEngine.Random.Range(-1000, 1000));
        tree4.transform.Rotate(Vector3.up * UnityEngine.Random.Range(-1000, 1000));
        tree5.transform.Rotate(Vector3.up * UnityEngine.Random.Range(-1000, 1000));
        tree6.transform.Rotate(Vector3.up * UnityEngine.Random.Range(-1000, 1000));
        tree1.transform.localPosition += new Vector3(UnityEngine.Random.Range(-1.5f, 1.5f), 0, UnityEngine.Random.Range(-.5f, .5f));
        tree2.transform.localPosition += new Vector3(UnityEngine.Random.Range(-1.5f, 1.5f), 0, UnityEngine.Random.Range(-.5f, .5f));
        tree3.transform.localPosition += new Vector3(UnityEngine.Random.Range(-1.5f, 1.5f), 0, UnityEngine.Random.Range(-.5f, .5f));
        tree4.transform.localPosition += new Vector3(UnityEngine.Random.Range(-1.5f, 1.5f), 0, UnityEngine.Random.Range(-.5f, .5f));
        tree5.transform.localPosition += new Vector3(UnityEngine.Random.Range(-1.5f, 1.5f), 0, UnityEngine.Random.Range(-.5f, .5f));
        tree6.transform.localPosition += new Vector3(UnityEngine.Random.Range(-1.5f, 1.5f), 0, UnityEngine.Random.Range(-.5f, .5f));

        Transform[] treelist = new Transform[6] { tree1, tree2, tree3, tree4, tree5, tree6 };
        Transform localp1 = treelist[UnityEngine.Random.Range(0, 6)];
        Transform localp2 = treelist[UnityEngine.Random.Range(0, 6)];
        Vector3 temp1;
        temp1 = localp1.localPosition;
        localp1.localPosition = localp2.localPosition;
        localp2.localPosition = temp1;

        Transform localp3 = treelist[UnityEngine.Random.Range(0, 6)];
        Transform localp4 = treelist[UnityEngine.Random.Range(0, 6)];
        temp1 = localp3.localPosition;
        localp3.localPosition = localp4.localPosition;
        localp4.localPosition = temp1;

        Transform localp5 = treelist[UnityEngine.Random.Range(0, 6)];
        Transform localp6 = treelist[UnityEngine.Random.Range(0, 6)];
        temp1 = localp5.localPosition;
        localp5.localPosition = localp6.localPosition;
        localp6.localPosition = temp1;
    }
    void Update() {
        
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x-1 , transform.position.y, transform.position.z ), speed * Time.deltaTime);
    }

}
