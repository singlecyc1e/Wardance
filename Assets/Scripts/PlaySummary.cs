using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySummary : MonoBehaviour
{
    public GameObject KillText;
    public GameObject DistanceText;
    public GameObject TitleCanvas;
    public GameObject SummaryButtom;
    public static bool IsStartMenu = true;

    private void Awake()
    {
        KillText.gameObject.SetActive(false);
        DistanceText.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", 0);
        }

        if (!PlayerPrefs.HasKey("BestDistance"))
        {
            PlayerPrefs.SetInt("BestDistance", 0);
        }

        KillText.transform.GetChild(0).gameObject.SetActive(false);
        DistanceText.transform.GetChild(0).gameObject.SetActive(false);

        if (IsStartMenu == true)
        {
            IsStartMenu = false;
            SummaryButtomCall();
        }
        else
        {
            DisplayPlaySummary();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisplayPlaySummary()
    {
        TitleCanvas.SetActive(false);

        KillText.gameObject.SetActive(true);
        DistanceText.SetActive(true);

        KillText.GetComponent<Text>().text = PlayerPrefs.GetInt("CurrentScore").ToString() + " Kills";
        DistanceText.GetComponent<Text>().text = PlayerPrefs.GetInt("CurrentDistance").ToString() + "m";

        //Update best scores
        if(PlayerPrefs.GetInt("CurrentScore") > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", PlayerPrefs.GetInt("CurrentScore"));
            NewKillRecord();
        }

        if (PlayerPrefs.GetInt("CurrentDistance") > PlayerPrefs.GetInt("BestDistance"))
        {
            PlayerPrefs.SetInt("BestDistance", PlayerPrefs.GetInt("CurrentDistance"));
            NewDistanceRecord();
        }
    }

    void NewKillRecord()
    {
        KillText.transform.GetChild(0).gameObject.SetActive(true);
    }

    void NewDistanceRecord()
    {
        DistanceText.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void SummaryButtomCall()
    {
        StartCoroutine(ReturnToStart());
    }

    IEnumerator ReturnToStart()
    {
        KillText.GetComponent<Animator>().SetTrigger("Play");
        DistanceText.GetComponent<Animator>().SetTrigger("Play");
        yield return new WaitForSeconds(.3f);
        KillText.SetActive(false);
        DistanceText.SetActive(false);
        TitleCanvas.SetActive(true);
    }
}
    