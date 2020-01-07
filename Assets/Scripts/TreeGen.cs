using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGen : MonoBehaviour
{
    public Transform treelocation;
    public GameObject treeobj;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        if (other.tag == "TreeGen")
        {
                Destroy(Instantiate(treeobj, other.gameObject.transform.position + new Vector3(83f, 0,  0), Quaternion.identity),18f);
        }
    }
}
