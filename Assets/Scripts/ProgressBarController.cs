using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour {
    private Slider slider;
    private int totalRoadCount;
    
    private void Start() {
        slider = GetComponent<Slider>();
        RoadManager.instance.InitProgressBar(this);
        totalRoadCount = RoadManager.instance.GetRoadCount();
    }

    public void Progress(int roadIndex) {
        var progress = (float) roadIndex / totalRoadCount;
        slider.value = progress;
    }
}
