using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    public Animator sub1_left;
    public Animator sub1_right;
    public Animator sub2;
    public Text t1;
    public GameObject TitleCanvas;
    public GameObject CreditsCanvas;

    public Animator Sword;
    public AudioSource SwordDraw;


    private void Start()
    {
        Time.timeScale = 1.0f;
        CreditsCanvas.SetActive(false);
    }

    public void LoadGame()
    {
        if (Sword != null)
        {
            Sword.SetTrigger("Open");
        }
        //Load next scene
        StartCoroutine(Startgame());
    }

    IEnumerator Startgame()
    {
        t1.enabled = false;
        sub1_left.SetTrigger("goleft");
        sub1_right.SetTrigger("goright");
        sub2.SetTrigger("fading");

        SwordDraw.Play();

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ShowCredits()
    {
        TitleCanvas.SetActive(false);
        CreditsCanvas.SetActive(true);
    }

    public void HideCredits()
    {
        TitleCanvas.SetActive(true);
        CreditsCanvas.SetActive(false);
    }
}
