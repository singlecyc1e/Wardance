using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
        this.transform.GetChild(0).gameObject.SetActive(false);
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
                Debug.Log(PlayerPrefs.GetInt(string.Format($"bestscore_{i}")));
            }
        }
    }

    void SetScore()
    {
        //for (int i = 0; i < 10; i++)
        //{
        //    PlayerPrefs.SetInt(string.Format("bestscore_{}", i), 10);

        //}
    }

    public void AddScore(int score)
    {

    }
}
