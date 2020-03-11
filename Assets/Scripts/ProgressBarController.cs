using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour {
    [Header("Stop road indices")] 
    public int[] stops;
    public int stopCount;
    public GameObject flag;

    private Slider slider;
    private int totalRoadCount;
    private float progress;
    private List<float> stopPercent;
    private int counter;
    private bool initialized;
    private int stopIndex;
    private int savedIndex;
    private bool stopped;
    private RectTransform rectTransform;
    private List<GameObject> flags;

    private void Start() {
        slider = GetComponent<Slider>();
        RoadManager.instance.InitProgressBar(this);
        totalRoadCount = RoadManager.instance.GetRoadCount();

        stopPercent = new List<float>();
        for (int i = 0; i < stops.Length; i++) {
            stopPercent.Add((float) stops[i] / totalRoadCount * (slider.maxValue - slider.minValue));
        }

        rectTransform = GetComponent<RectTransform>();
        var width = rectTransform.rect.width;
        // var left = rectTransform.anchoredPosition.x;
        flags = new List<GameObject>();
        for (int i = 0; i < stops.Length; i++) {
            var x = stopPercent[i] * width;
            var spawned = Instantiate(flag, transform);
            var spawnedRectTrans = spawned.GetComponent<RectTransform>();
            spawnedRectTrans.anchoredPosition = new Vector2(x, spawnedRectTrans.anchoredPosition.y);
            flags.Add(spawned);
        }

        counter = 0;
        initialized = false;
        stopIndex = 0;
        stopped = false;
        savedIndex = 0;
    }

    public void Progress(int roadIndex) {
        if (stopped) {
            if (roadIndex > savedIndex) {
                ++counter;
                if (counter >= stopCount) {
                    counter = 0;
                    flags[stopIndex].GetComponent<Image>().color = Color.red;
                    ++stopIndex;
                    stopped = false;
                } else {
                    return;
                }
            } else {
                return;
            }
        }

        progress = (float) roadIndex / totalRoadCount * (slider.maxValue - slider.minValue);
        if (stopIndex < stopPercent.Count && progress >= stopPercent[stopIndex]) {
            savedIndex = roadIndex;
            stopped = true;
        }
        
        if (!initialized) {
            slider.value = progress;
            Debug.Log(slider.value);
            int i;
            for (i = 0; i < stopPercent.Count; i++) {
                if (stopPercent[i] - progress > 0.1f) break;
                flags[i].GetComponent<Image>().color = Color.red;
            }

            stopIndex = i;
            initialized = true;
        }
    }

    private void Update() {
        if (stopped) return;
        
        slider.value = Mathf.MoveTowards(slider.value, progress, 0.1f * (progress - slider.value));
        
    }
}