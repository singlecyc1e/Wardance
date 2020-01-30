using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScores : MonoBehaviour
{
    public Text BestScoreKillText;
    //public Text BestScoreDistanceText;

    private int BestScoreKill = 0;
    //private int BestScoreDistance = 0;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScoreDisplay()
    {
        BestScoreKill = (int)GameObject.Find("SwordCollider").GetComponent<WeaponDMG>().killscore;

        if (BestScoreKill < PlayerPrefs.GetInt(string.Format("BestKill")))
        {
            BestScoreKill = PlayerPrefs.GetInt(string.Format("BestKill"));
        }
        else
        {
            PlayerPrefs.SetInt(string.Format("BestKill"), BestScoreKill);
        }
        
        //Update BestScoreDistance

        BestScoreKillText.text = BestScoreKill.ToString();
        //Update BestScoreDistanceText
    }

    private void Init()
    {
        if (!PlayerPrefs.HasKey(string.Format("BestKill")))
        {
            PlayerPrefs.SetInt(string.Format("BestKill"), 0);
        }

        if (!PlayerPrefs.HasKey(string.Format("BestDistance")))
        {
            PlayerPrefs.SetInt(string.Format("BestDistance"), 0);
        }
    }
}
