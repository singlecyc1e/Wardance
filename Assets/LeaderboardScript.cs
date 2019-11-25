using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("bestscore_9", 0);
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
        
    }

    void SetScore()
    {
        //for (int i = 0; i < 10; i++)
        //{
        //    PlayerPrefs.SetInt(string.Format("bestscore_{}", i), 10);

        //}
    }

    public void Refresh()
    {

    }
}
