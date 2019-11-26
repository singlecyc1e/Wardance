using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void Reset()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        SceneManager.LoadScene(0);
        WeaponDMG.instance.Alive = true;
        WeaponDMG.instance.killscore = 0;
    }

    public void SubmitScore()
    {
        int Score = (int)GameObject.Find("SwordCollider").GetComponent<WeaponDMG>().killscore;
        GameObject.Find("UI_Leaderboard").GetComponent<LeaderboardScript>().AddScore(Score);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
