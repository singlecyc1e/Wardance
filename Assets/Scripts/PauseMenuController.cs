using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour {

    public void ToggleControlMethod(bool useButtonControl) {
        // TODO
    }
    
    public void Resume() {
        Time.timeScale = 1f;
    }

    public void Quit() {
        Application.Quit();
    }
}
