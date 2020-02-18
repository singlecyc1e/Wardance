using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageSystem : MonoBehaviour {
    public float maxRageValue = 15;
    public bool RageState = false;
    public float LastingTime = 10f;
    
    public Text RageText;
    public Image RageBar;
    public float decreasing_rate_normal = .0005f;
    public float decreasing_rate_rage = .0025f;
    
    private float rageValue = 0;
    private bool maxRage;
    private GameObject Sword;
    private GameObject Sword2;
    private GameObject rageSowrd;
    private GameObject rageSowrd2;
    
    public static RageSystem instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        RageBar.fillAmount = 0f;
        maxRage = false;
        
        Sword = GameObject.Find("swordmodel").gameObject;
        //Sword2 = GameObject.Find("normalsword_2").gameObject;
        rageSowrd = GameObject.Find("Rageswordmodel").gameObject;
        //rageSowrd2 = GameObject.Find("RageSword_2").gameObject;
        rageSowrd.SetActive(false);
        //rageSowrd2.SetActive(false);
    }

    private void Update() {
        if (!RageState) {
            RageBar.color = Color.HSVToRGB(0, RageBar.fillAmount / 1.5f + 0.4f, .9f);
            //RageBar.color = Color.HSVToRGB(0, RageBar.fillAmount * 100, 100);
            if (RageBar.fillAmount >= 0.95) {
                maxRage = true;
                RageBar.color = Color.HSVToRGB(0, 1, .9f);
            }
        } else {
            if (RageBar.fillAmount <= 0) {
                DeactivateRage();
            }
        }
    }

    private void FixedUpdate() {
        if (!RageState) {
            RageBar.fillAmount -= decreasing_rate_normal;
        } else {
            RageBar.fillAmount -= decreasing_rate_rage;
        }
    }

    public void AddRageValue() {
        if (!RageState) {
            if(maxRage) return;
            
            rageValue += 1;
            RageText.text = rageValue.ToString() + "/" + maxRageValue.ToString() + "(only for debug)";
            RageBar.fillAmount += .1f;
        } else {
            RageBar.fillAmount += .1f;
        }
    }

    public void ActivateRage() {
        if(!maxRage) return;

        maxRage = false;
        RageState = true;
        Sword.SetActive(false);
        //Sword2.SetActive(false);
        rageSowrd.SetActive(true);
        //rageSowrd2.SetActive(true);
    }

    private void DeactivateRage() {
        rageSowrd.SetActive(false);
        //rageSowrd2.SetActive(false);
        Sword.SetActive(true);
        //Sword2.SetActive(true);
        RageState = false;
    }

    // public float ShowRageValue() {
    //     return rageValue;
    // }

    // public void Reset() {
    //     rageValue = 0;
    // }

    // IEnumerator RageMode() {
    //     RageState = true;
    //     Sword.SetActive(false);
    //     //Sword2.SetActive(false);
    //     rageSowrd.SetActive(true);
    //     //rageSowrd2.SetActive(true);
    //     yield return new WaitForSeconds(LastingTime);
    //     rageSowrd.SetActive(false);
    //     //rageSowrd2.SetActive(false);
    //     Sword.SetActive(true);
    //     //Sword2.SetActive(true);
    //     RageState = false;
    // }
}