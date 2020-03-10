using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class RespawnMenuManager : MonoBehaviour
{
    public Animator sub1;
    public Animator sub2;
    public Text t1;

    public GameObject Life1;
    public GameObject Life2;
    public GameObject Life3;

    public GameObject Buttom;

    public AudioSource SwordDraw;

    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    public void LoadGame()
    {
        Buttom.SetActive(false);

        //Load next scene
        StartCoroutine(Startgame());
    }

    IEnumerator Startgame()
    {
        t1.enabled = false;

        sub1.SetTrigger("fading");
        sub2.SetTrigger("fading");
        SwordDraw.Play();

        yield return new WaitForSeconds(.5f);

        Life1.SetActive(false);
        Life2.SetActive(false);
        Life3.SetActive(false);

        SceneManager.LoadScene("DistanceTest");
    }
}
