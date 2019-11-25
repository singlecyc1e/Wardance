﻿using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

public class SwipeKillController : MonoBehaviour {
    public float minimumSwipeSpeed;
    
    private bool willCount = false;
    private int counter;
    private int frameCounter;
    
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

    private void FixedUpdate() {
        var fingers = LeanTouch.Fingers;
        foreach (var finger in fingers) {
            if (finger.ScreenDelta.magnitude / Time.deltaTime > minimumSwipeSpeed) {
                ++frameCounter;
            }
        }

        if (frameCounter >= 20) {
            ++counter;
            Debug.Log(counter);
            frameCounter = 0;
        }
    }
}
