using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGen : MonoBehaviour
{
    private bool created = false;
    private void OnTriggerEnter(Collider other)
    {
        if (created == false)
        {
            Debug.Log(other.tag);
            if (other.tag == "TreeGen")
            {

                created = true;

                Instantiate(transform.parent, transform.parent.position + new Vector3(50, 0, 0), Quaternion.identity);
                Debug.Log(transform.parent.name);
            }
        }
    }
}
