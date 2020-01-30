using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullCollecting : MonoBehaviour
{
    public GameObject Skull;
    public Transform targetPosition;
    public int skullnum = 5;

    public void CreateSkull()
    {
        for (int num = 0; num < skullnum; num++)
        {
            GameObject.Instantiate(Skull, this.transform); //assign the targetPosition to the skull 

            /// question 1. should i instantiate gameobjects or just images?
            /// when adds to num left top, the num will shake from small to big
        }
        
    }
    
}
