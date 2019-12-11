using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public GameObject treeblocks;
    public static TreeManager instance;
    private void Awake()
    {
        if (instance != null) { instance = this; }
    }

    // Start is called before the first frame update
    void Start()
    {

        //left
        Instantiate(treeblocks, treeblocks.transform.localPosition + new Vector3(0, 0, +4), Quaternion.identity);
        Instantiate(treeblocks, treeblocks.transform.localPosition + new Vector3(0, 0, +8), Quaternion.identity);
        Instantiate(treeblocks, treeblocks.transform.localPosition + new Vector3(0, 0, +12), Quaternion.identity);
        //right
        Instantiate(treeblocks, treeblocks.transform.localPosition + new Vector3(0, 0, -15), Quaternion.identity);
        Instantiate(treeblocks, treeblocks.transform.localPosition + new Vector3(0, 0, -19), Quaternion.identity);
        Instantiate(treeblocks, treeblocks.transform.localPosition + new Vector3(0, 0, -23), Quaternion.identity);
        Instantiate(treeblocks, treeblocks.transform.localPosition + new Vector3(0, 0, -27), Quaternion.identity);
    }

}
