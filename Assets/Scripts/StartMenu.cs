using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public static StartMenu instance;
    public bool STARTmenu = true;
    public Animator myAnimationController;

    private void Awake()
    {
       
    }
    private void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Start Menu");
        if (objs.Length > 1) { Destroy(this.gameObject); }
        DontDestroyOnLoad(this.gameObject);

        if (instance == null) { instance = this; }
        if (STARTmenu)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(ExampleCoroutine());


        }
    }
    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0;
    }

    IEnumerator Fogstart()
    {
        yield return new WaitForSecondsRealtime(0f);
        this.transform.GetChild(0).gameObject.SetActive(false);
        Time.timeScale = 1;
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
        STARTmenu = false;
        if (myAnimationController)
        {
            myAnimationController.SetBool("move", true);
        }
            
        StartCoroutine(Fogstart());
        this.transform.GetChild(0).gameObject.SetActive(false);
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
