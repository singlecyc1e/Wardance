using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    public Animator sub1;
    public Animator sub2;
    public Text t1;

    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    public void LoadGame()
    {
        //Load next scene
        StartCoroutine(Startgame());
    }

    IEnumerator Startgame()
    {
        t1.enabled = false;
        sub1.SetTrigger("fading");
        sub2.SetTrigger("fading");
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
