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

    private GameObject Sword;
    private GameObject Sword2;
    private GameObject rageSowrd;
    private GameObject rageSowrd2;

    private void Start()
    {
        Sword = GameObject.Find("normalsword").gameObject;
        Sword2 = GameObject.Find("normalsword_2").gameObject;
        rageSowrd = GameObject.Find("RageSword").gameObject;
        rageSowrd2 = GameObject.Find("RageSword_2").gameObject;
        rageSowrd.SetActive(false);
        rageSowrd2.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (!RageState)
        {
            if (RageValue == RageMaxValue)
            {
                StartCoroutine(RageMode());
                Reset();
            }
        }
    }

    public void AddRageValue()
    {
        if (!RageState)
        {
            RageValue += 1;
            RageText.text = RageValue.ToString();
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
        Sword2.SetActive(false);
        rageSowrd.SetActive(true);
        rageSowrd2.SetActive(true);
        yield return new WaitForSeconds(LastingTime);
        rageSowrd.SetActive(false);
        rageSowrd2.SetActive(false);
        Sword.SetActive(true);
        Sword2.SetActive(true);
        RageState = false;
    }
}
