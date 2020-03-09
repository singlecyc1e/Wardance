using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour {
    [Header("Stop road indices")]
    public int[] stops;
    public float stopTime;
    
    private Slider slider;
    private int totalRoadCount;
    private float progress;
    private List<float> stopPercent;
    private float counter;
    private bool initialized;
    private int stopIndex;
    private bool stopped;
    
    private void Start() {
        slider = GetComponent<Slider>();
        RoadManager.instance.InitProgressBar(this);
        totalRoadCount = RoadManager.instance.GetRoadCount();

        stopPercent = new List<float>();
        for (int i = 0; i < stops.Length; i++) {
            stopPercent.Add((float) stops[i] / totalRoadCount * (slider.maxValue - slider.minValue));
        }

        counter = 0;
        initialized = false;
        stopIndex = 0;
        stopped = false;
    }

    public void Progress(int roadIndex) {
        progress = (float) roadIndex / totalRoadCount * (slider.maxValue - slider.minValue);
        if (!initialized) {
            slider.value = progress;
            int i;
            for (i = 0; ; i++) {
                if(stopPercent[i] > progress) break;
            }

            stopIndex = i;
            initialized = true;
        }
    }

    private void Update() {
        if (stopped) {
            counter += Time.deltaTime;
            if (counter >= stopTime) {
                stopped = false;
            }
        } else {
            slider.value = Mathf.MoveTowards(slider.value, progress, 0.1f * (progress - slider.value));
            if (slider.value >= stopPercent[stopIndex]) {
                stopped = true;
            }
        }
        
    }
}
