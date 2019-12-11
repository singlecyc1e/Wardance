using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class LeaderboardScript : MonoBehaviour
{
    public Text LeaderboardText0;
    public Text LeaderboardText1;
    public Text LeaderboardText2;
    public Text LeaderboardText3;
    public Text LeaderboardText4;
    public Text LeaderboardText5;
    public Text LeaderboardText6;
    public Text LeaderboardText7;
    public Text LeaderboardText8;
    public Text LeaderboardText9;

    List<int> RankList;
    Dictionary<int, string> RankDict;

    // Start is called before the first frame update
    void Start()
    {
        RankList = new List<int>();
        RankDict = new Dictionary<int, string>();

        RankList.Clear();
        RankDict.Clear();

        Init();
        LoadToList();

        UpdateLeaderboardText();

        //this.transform.GetChild(0).gameObject.SetActive(false);

        //for (int i = 0; i < 10; i++)
        //{
        //    if (PlayerPrefs.HasKey(string.Format($"bestscore_{i}")))
        //    {
        //        Debug.Log(PlayerPrefs.GetInt(string.Format($"bestscore_{i}")));
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
        for (int i = 0; i < 10; i++)
        {
            if (!PlayerPrefs.HasKey(string.Format($"bestscore_{i}")))
            {
                PlayerPrefs.SetInt(string.Format($"bestscore_{i}"), 0);
            }

            if (!PlayerPrefs.HasKey(string.Format($"bestplayer_{i}")))
            {
                PlayerPrefs.SetString(string.Format($"bestplayer_{i}"), "NoName");
            }
        }
    }

    public void AddScore(int Score, string Player)
    {
        RankList.Sort();
        RankList.RemoveAt(0);
        RankList.Add(Score);
        RankList.Sort();
        RankList.Reverse();

        if (!RankDict.ContainsKey(Score))
        {
            RankDict.Add(Score, Player);
        }

        UpdatePlayerPrefs();
        UpdateLeaderboardText();
    }

    private void UpdatePlayerPrefs()
    {
        for (int i = 0; i < 10; i++)
        {
            if (PlayerPrefs.HasKey(string.Format($"bestscore_{i}")))
            {
                PlayerPrefs.SetInt(string.Format($"bestscore_{i}"), RankList[i]);
            }

            if (PlayerPrefs.HasKey(string.Format($"bestplayer_{i}")))
            {
                PlayerPrefs.SetString(string.Format($"bestplayer_{i}"), RankDict[RankList[i]]);
                //Debug.Log(PlayerPrefs.GetInt(string.Format($"bestscore_{i}")));
            }
        }
    }

    private void LoadToList()
    {
        RankList.Clear();

        for (int i = 0; i < 10; i++)
        {
            if (PlayerPrefs.HasKey(string.Format($"bestscore_{i}")))
            {
                int BestScore = PlayerPrefs.GetInt(string.Format($"bestscore_{i}"));
                string BestPlayer = PlayerPrefs.GetString(string.Format($"bestplayer_{i}"));

                RankList.Add(BestScore);

                if (!RankDict.ContainsKey(BestScore))
                {
                    RankDict.Add(BestScore, BestPlayer);
                }     
            }
        }
    }

    private void UpdateLeaderboardText()
    {
        LeaderboardText0.text = string.Format("{0} . {1, 4}", "1", RankList[0]);
        LeaderboardText1.text = string.Format("{0} . {1, 4}", "2", RankList[1]);
        LeaderboardText2.text = string.Format("{0} . {1, 4}", "3", RankList[2]);
        LeaderboardText3.text = string.Format("{0} . {1, 4}", "4", RankList[3]);
        LeaderboardText4.text = string.Format("{0} . {1, 4}", "5", RankList[4]);
        LeaderboardText5.text = string.Format("{0} . {1, 4}", "6", RankList[5]);
        LeaderboardText6.text = string.Format("{0} . {1, 4}", "7", RankList[6]);
        LeaderboardText7.text = string.Format("{0} . {1, 4}", "8", RankList[7]);
        LeaderboardText8.text = string.Format("{0} . {1, 4}", "9", RankList[8]);
        LeaderboardText9.text = "";
    }
}
