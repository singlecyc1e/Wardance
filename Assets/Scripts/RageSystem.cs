﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageSystem : MonoBehaviour {
    public float maxRageValue;
    public bool inRageMode;
    public float LastingTime = 10f;
    
    public Text rageText;
    public Image rageBarFill;
    public float decreasing_rate_normal = .0005f;
    public float decreasing_rate_rage = .0025f;
    public Animator swordAnimator;
    public GameObject traileffectlight;
    public GameObject traileffectheavy;

    private float rageValue;
    private bool hasMaxRage;
    
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
        rageValue = 0f;
        hasMaxRage = false;
        inRageMode = false;
        UpdateRageDisplay();
        
        Sword = GameObject.Find("swordmodel").gameObject;
        //Sword2 = GameObject.Find("normalsword_2").gameObject;
        rageSowrd = GameObject.Find("Rageswordmodel").gameObject;
        //rageSowrd2 = GameObject.Find("RageSword_2").gameObject;
        rageSowrd.SetActive(false);
        //rageSowrd2.SetActive(false);
    }

    private void FixedUpdate() {
        if (hasMaxRage) return;
        
        if (inRageMode) {
            rageValue -= decreasing_rate_rage;
            if (rageValue <= 0) {
                DeactivateRage();
                rageValue = 0f;
            }
        } else {
            if (rageValue > 0f) {
                rageValue -= decreasing_rate_normal;
            }
        }
        
        UpdateRageDisplay();
    }

    public void AddRageValue() {
        if(hasMaxRage || inRageMode) return;
        rageValue += 1f;
        if (rageValue >= maxRageValue || Mathf.Abs(rageValue - maxRageValue) <= 0.2) {
            hasMaxRage = true;
            rageValue = maxRageValue;
        }
        UpdateRageDisplay();
    }

    public void ActivateRage() {
        Debug.Log("1111");
        if(!hasMaxRage) return;

        ScannerController.instance.CheckAndScan();
        hasMaxRage = false;
        inRageMode = true;

        swordAnimator.SetBool("rageMode", true);
        Sword.SetActive(false);
        //Sword2.SetActive(false);
        rageSowrd.SetActive(true);
        //rageSowrd2.SetActive(true);
        traileffectlight.SetActive(false);
        traileffectheavy.SetActive(true);
    }


    private void DeactivateRage() {
        inRageMode = false;
        hasMaxRage = false;
        rageValue = 0f;
        ScannerController.instance.CheckAndScan();
        swordAnimator.SetBool("rageMode", false);
        rageSowrd.SetActive(false);
        //rageSowrd2.SetActive(false);
        Sword.SetActive(true);
        //Sword2.SetActive(true);
        traileffectlight.SetActive(true);
        traileffectheavy.SetActive(false);
    }

    private void UpdateRageDisplay() {
        rageBarFill.fillAmount = rageValue / maxRageValue;
        rageBarFill.color = hasMaxRage ? Color.HSVToRGB(0, 1, .9f) : Color.HSVToRGB(0, rageBarFill.fillAmount / 1.5f + 0.4f, .9f);
        rageText.text = rageValue + "/" + maxRageValue + "(only for debug)";
    }
}