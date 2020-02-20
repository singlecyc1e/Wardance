using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageSystem : MonoBehaviour
{
    private int RageValue = 0;
    public int RageMaxValue = 15;
    public bool RageState = false;
    public float LastingTime = 10f;
    public Text RageText;
    public static RageSystem instance;
    public Image RageBar;
    public float decreasing_rate_normal = .0002f;
    public float decreasing_rate_rage = .0025f;
    public Animator sword;
    private GameObject Sword;
    private GameObject Sword2;
    private GameObject rageSowrd;
    private GameObject rageSowrd2;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        RageBar.fillAmount = 0f;
        Sword = GameObject.Find("swordmodel").gameObject;
        //Sword2 = GameObject.Find("normalsword_2").gameObject;
        rageSowrd = GameObject.Find("Rageswordmodel").gameObject;
        //rageSowrd2 = GameObject.Find("RageSword_2").gameObject;
        rageSowrd.SetActive(false);
        //rageSowrd2.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (!RageState)
        {
            //Debug.Log(RageBar.fillAmount);
            RageBar.color = Color.HSVToRGB(0, RageBar.fillAmount/1.5f + 0.4f, .9f);
            //RageBar.color = Color.HSVToRGB(0, RageBar.fillAmount * 100, 100);
            if (RageBar.fillAmount >= 0.95)
            {
                RageBar.color = Color.HSVToRGB(0, 1, .9f);
                //StartCoroutine(RageMode());
                RageState = true;
                sword.SetBool("rageMode", true);

                Sword.SetActive(false);
                //Sword2.SetActive(false);
                rageSowrd.SetActive(true);
                //rageSowrd2.SetActive(true);
                //Reset();
            }
        }
        else
        {
            if (RageBar.fillAmount <= 0)
            {
                sword.SetBool("rageMode", false);
                rageSowrd.SetActive(false);
                //rageSowrd2.SetActive(false);
                Sword.SetActive(true);
                //Sword2.SetActive(true);
                RageState = false;
                
            }
        }
    }

    private void FixedUpdate()
    {
        if (!RageState)
        {
            RageBar.fillAmount -= decreasing_rate_normal;
        }
        else
        {
            RageBar.fillAmount -= decreasing_rate_rage;
        }
        
    }

    public void AddRageValue()
    {
        if (!RageState)
        {
            RageValue += 1;
            RageText.text = RageValue.ToString()+"/" + RageMaxValue.ToString() + "(only for debug)";
            RageBar.fillAmount += .1f;
        }
        else
        {
            RageBar.fillAmount += .1f;
        }
    }

    public int ShowRageValue()
    {
        return RageValue;
    }

    public void Reset()
    {
        RageValue = 0;
    } 

    IEnumerator RageMode()
    {
        RageState = true;
        Sword.SetActive(false);
        //Sword2.SetActive(false);
        rageSowrd.SetActive(true);
        //rageSowrd2.SetActive(true);
        yield return new WaitForSeconds(LastingTime);
        rageSowrd.SetActive(false);
        //rageSowrd2.SetActive(false);
        Sword.SetActive(true);
        //Sword2.SetActive(true);
        RageState = false;
    }
}
