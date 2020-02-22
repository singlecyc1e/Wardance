using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {
    public SwipeKillController swipeKillController;

    public static string useButtonSettingKey = "USE_BUTTON";
    public static LevelController instance;
    
    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("Try to load two LevelManager.");
        }

        Application.targetFrameRate = 60;
    }

    private void Start() {
        RoadManager.instance.Init(5);
        PlayerPrefs.SetInt(useButtonSettingKey, 0);
    }

    public void StartSwipeCounting() {
        swipeKillController.StartCounting();
    }

    public int GetSwipeResultAndClear() {
        return swipeKillController.GetResultAndClear();
    }
}
