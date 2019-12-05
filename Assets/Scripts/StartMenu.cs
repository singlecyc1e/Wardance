using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public static StartMenu instance;
    public bool STARTmenu = true;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Start Menu");
        if (objs.Length > 1) { Destroy(this.gameObject); }
        DontDestroyOnLoad(this.gameObject);

        if (instance == null) { instance = this; }
        
        if (STARTmenu) {
            this.transform.GetChild(0).gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void BackToMenu()
    {
        STARTmenu = true;
        this.transform.GetChild(0).gameObject.SetActive(true);
        Time.timeScale = 0;
        SceneManager.LoadScene(0);
    }
    public void MyStart()
    {
        Debug.Log("mystart!");
        STARTmenu = false;
        this.transform.GetChild(0).gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Credits()
    {
        //display
    }

    public void Quit()
    {
        Application.Quit();
    }
}
