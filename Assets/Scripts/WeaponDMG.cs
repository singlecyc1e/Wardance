﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDMG : MonoBehaviour
{
    public Text UI_killscore; 
    public float killscore = 0;
    public static WeaponDMG instance;
    public bool Alive = true;
    public TimeController timeManager;
    public bool BulletTime = false;
    //public TimeController Timemanager;
    // Start is called before the first frame update

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        Alive = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Block")
        {
            //pause game
            Time.timeScale = 0;
            GameObject.Find("Death Menu").transform.GetChild(0).gameObject.SetActive(true);
            Alive = false;
        }

        if (other.gameObject.tag == "Regular")
        {
            
            //if "move" in playercontroller is True
            if (PlayerController.instance.moving | RageSystem.instance.RageState | PlayerController.instance.slashing)
            {
                killscore += 1;
                UI_killscore.text = killscore.ToString() + "人斩";
                Destroy(other.gameObject.GetComponent<MeshRenderer>());
                other.gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
                Destroy(other.gameObject, 3);// destroy the Enemy and play destroy deconstruction animation;
                RageSystem.instance.AddRageValue();
                if (BulletTime)
                {
                    timeManager.BulletTime();
                }
            }
            else
            {
                //pause game
                
                Time.timeScale = 0;
                GameObject.Find("Death Menu").transform.GetChild(0).gameObject.SetActive(true);
                Alive = false;
            }
        }

        if (other.gameObject.tag == "HeavyArmor")
        {

            //if "move" in playercontroller is True
            if (RageSystem.instance.RageState)
            {
                killscore += 1;
                UI_killscore.text = killscore.ToString() + "人斩";
                Destroy(other.gameObject.GetComponent<MeshRenderer>());
                other.gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
                Destroy(other.gameObject, 3);// destroy the Enemy and play destroy deconstruction animation;
                RageSystem.instance.AddRageValue();
                if (BulletTime)
                {
                    timeManager.BulletTime();
                }
            }
            else
            {
                //pause game
                Time.timeScale = 0;

                GameObject.Find("Death Menu").transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("UI_Leaderboard").transform.GetChild(0).gameObject.SetActive(true);
                Alive = false;

                if (PlayerPrefs.HasKey("bestscore_9"))
                {
                    if (PlayerPrefs.GetInt("bestscore_9") < killscore)
                    {
                        GameObject.Find("SubmitScore").SetActive(true);
                        GameObject.Find("LeaderboardInput").SetActive(true);
                    }
                    else
                    {
                        GameObject.Find("SubmitScore").SetActive(false);
                        GameObject.Find("LeaderboardInput").SetActive(false);
                    }
                }

            }
        }
        //if "move" in playercontroller is False
        //gameover, pause the game
    }
}
