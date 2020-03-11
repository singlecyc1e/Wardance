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

    private void Start() {
        buttonControlCanvas.SetActive(PlayerPrefs.GetInt(LevelController.useButtonSettingKey) == 1);
    }

    public void ClearAllData() {
        PlayerPrefs.DeleteAll();
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
