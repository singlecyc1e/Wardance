using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour {
    public Toggle useButtonToggle;
    public GameObject buttonControlCanvas;
    
    private void OnEnable() {
        useButtonToggle.isOn =
            PlayerPrefs.GetInt(LevelController.useButtonSettingKey) == 1;
        useButtonToggle.onValueChanged.AddListener(activate => {
            PlayerPrefs.SetInt(LevelController.useButtonSettingKey, activate ? 1 : 0);
            buttonControlCanvas.SetActive(activate);
        });
    }

    public void ToggleControlMethod(bool useButtonControl) {
        // TODO
    }
    
    public void Pause() {
        Time.timeScale = 0f;
    }
    
    public void Resume() {
        Time.timeScale = 1f;
    }

    public void Quit() {
        Application.Quit();
    }
}
