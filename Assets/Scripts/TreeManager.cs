using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public GameObject treeblocks;
    public Transform location;
    public static TreeManager instance;
    private void Awake()
    {
        if (instance != null) { instance = this; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //left
        Destroy(Instantiate(treeblocks, location.position, Quaternion.identity),5f);
        Destroy(Instantiate(treeblocks, location.position + new Vector3(0, 0, +4), Quaternion.identity), 5f);
        Destroy(Instantiate(treeblocks, location.position + new Vector3(0, 0, +8), Quaternion.identity), 5f);
        Destroy(Instantiate(treeblocks, location.position + new Vector3(0, 0, +12), Quaternion.identity), 5f);
        //right
        Destroy(Instantiate(treeblocks, location.position + new Vector3(0, 0, -15), Quaternion.identity), 5f);
        Destroy(Instantiate(treeblocks, location.position + new Vector3(0, 0, -19), Quaternion.identity), 5f);
        Destroy(Instantiate(treeblocks, location.position + new Vector3(0, 0, -23), Quaternion.identity), 5f);
        Destroy(Instantiate(treeblocks, location.position + new Vector3(0, 0, -27), Quaternion.identity), 5f);
    }

    public void Reset()
    {
        //left
        Destroy(Instantiate(treeblocks, location.position, Quaternion.identity), 5f);
        Destroy(Instantiate(treeblocks, location.position + new Vector3(0, 0, +4), Quaternion.identity), 5f);
        Destroy(Instantiate(treeblocks, location.position + new Vector3(0, 0, +8), Quaternion.identity), 5f);
        Destroy(Instantiate(treeblocks, location.position + new Vector3(0, 0, +12), Quaternion.identity), 5f);
        //right
        Destroy(Instantiate(treeblocks, location.position + new Vector3(0, 0, -15), Quaternion.identity), 5f);
        Destroy(Instantiate(treeblocks, location.position + new Vector3(0, 0, -19), Quaternion.identity), 5f);
        Destroy(Instantiate(treeblocks, location.position + new Vector3(0, 0, -23), Quaternion.identity), 5f);
        Destroy(Instantiate(treeblocks, location.position + new Vector3(0, 0, -27), Quaternion.identity), 5f);
    }

}
