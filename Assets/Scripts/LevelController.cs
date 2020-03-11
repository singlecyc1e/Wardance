using UnityEngine;

public class LevelController : MonoBehaviour {
    public SwipeKillController swipeKillController;

    public static string useButtonSettingKey = "USE_BUTTON";
    public static string playerLifeKey = "PLAYER_LIFE";
    public static string finishTutorialKey = "FINISH_TUTORIAL";
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
        RoadManager.instance.Init();
        if(!PlayerPrefs.HasKey(useButtonSettingKey)) {
            PlayerPrefs.SetInt(useButtonSettingKey, 0);
        }
        // PlayerPrefs.SetInt(useButtonSettingKey, 0);
        if(!PlayerPrefs.HasKey(playerLifeKey)) {
            PlayerPrefs.SetInt(playerLifeKey, 3);
        }
        
        if(!PlayerPrefs.HasKey(finishTutorialKey)) {
            PlayerPrefs.SetInt(finishTutorialKey, 0);
        }
    }

    public static void DecrementLife() {
        var lifeRemain = PlayerPrefs.GetInt(playerLifeKey);
        // Debug.Log("decrement " + lifeRemain);
        if(lifeRemain <= 1) {
            WeaponDMG.instance.SetupDeathMenu();
            PlayerPrefs.SetInt(playerLifeKey, 3);
            RoadManager.instance.Save(0);
            // PlayerPrefs.SetInt(finishTutorialKey, 0);
        } else {
            PlayerPrefs.SetInt(playerLifeKey, lifeRemain - 1);
            WeaponDMG.instance.SetupResponseMenu();
        }
    }

    public void StartSwipeCounting() {
        swipeKillController.StartCounting();
    }

    public int GetSwipeResultAndClear() {
        return swipeKillController.GetResultAndClear();
    }
}
