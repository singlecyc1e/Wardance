using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Death Menu");
        if (objs.Length > 1) { Destroy(this.gameObject); }
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        DontDestroyOnLoad(this);
    }

    public void Reset()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("UI_Leaderboard").transform.GetChild(0).gameObject.SetActive(false);
        SceneManager.LoadScene(0);  
        StartMenu.instance.STARTmenu = false;
        StartMenu.instance.MyStart();
    }

    public void BackMain()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void SubmitScore()
    {
        int Score = (int)GameObject.Find("SwordCollider").GetComponent<WeaponDMG>().killscore;
        //string Player = GameObject.Find("LeaderboardInputText").GetComponent<Text>().text;
        
        GameObject.Find("UI_Leaderboard").GetComponent<LeaderboardScript>().AddScore(Score, "NoName");

        GameObject.Find("SubmitScore").transform.GetChild(0).gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
