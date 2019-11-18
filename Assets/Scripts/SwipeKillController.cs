using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeKillController : MonoBehaviour {
    private bool willCount = false;
    private int counter;
    
    public void StartCounting() {
        willCount = true;
    }

    public void OnSwipeDetected() {
        if(!willCount) return;
        
        ++counter;
    }

    public int GetResultAndClear() {
        var result = counter;
        counter = 0;
        willCount = false;
        return result;
    }
}
