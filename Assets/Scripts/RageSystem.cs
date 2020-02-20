using System.Collections;
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
        UpdateRageDisplay();
        
        Sword = GameObject.Find("swordmodel").gameObject;
        //Sword2 = GameObject.Find("normalsword_2").gameObject;
        rageSowrd = GameObject.Find("Rageswordmodel").gameObject;
        //rageSowrd2 = GameObject.Find("RageSword_2").gameObject;
        rageSowrd.SetActive(false);
        //rageSowrd2.SetActive(false);
    }

    // private void Update() {
    //     
    //     if (!inRageMode) {
    //         rageBarFill.color = Color.HSVToRGB(0, rageBarFill.fillAmount / 1.5f + 0.4f, .9f);
    //         //rageBarFill.color = Color.HSVToRGB(0, rageBarFill.fillAmount * 100, 100);
    //         if (rageBarFill.fillAmount >= 0.95) {
    //             hasMaxRage = true;
    //             rageBarFill.color = Color.HSVToRGB(0, 1, .9f);
    //         }
    //     } else {
    //         if (rageBarFill.fillAmount <= 0) {
    //             DeactivateRage();
    //         }
    //     }
    // }

    private void FixedUpdate() {
        if(hasMaxRage) return;
        
        if (inRageMode) {
            rageValue -= decreasing_rate_rage;
            // rageBarFill.fillAmount -= decreasing_rate_normal;
        } else {
            rageValue -= decreasing_rate_normal;
            if (rageValue <= 0) {
                DeactivateRage();
                rageValue = 0f;
            }
            // rageBarFill.fillAmount -= decreasing_rate_rage;
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
        if(!hasMaxRage) return;

        swordAnimator.SetBool("rageMode", true);
        hasMaxRage = false;
        inRageMode = true;
        Sword.SetActive(false);
        //Sword2.SetActive(false);
        rageSowrd.SetActive(true);
        //rageSowrd2.SetActive(true);
    }

    private void DeactivateRage() {
        inRageMode = false;
        
        swordAnimator.SetBool("rageMode", false);
        rageSowrd.SetActive(false);
        //rageSowrd2.SetActive(false);
        Sword.SetActive(true);
        //Sword2.SetActive(true);
        inRageMode = false;
    }

    private void UpdateRageDisplay() {
        rageBarFill.fillAmount = rageValue / maxRageValue;
        rageBarFill.color = hasMaxRage ? Color.HSVToRGB(0, 1, .9f) : Color.HSVToRGB(0, rageBarFill.fillAmount / 1.5f + 0.4f, .9f);
        rageText.text = rageValue + "/" + maxRageValue + "(only for debug)";
        
    }

    // public float ShowRageValue() {
    //     return rageValue;
    // }

    // public void Reset() {
    //     rageValue = 0;
    // }

    // IEnumerator RageMode() {
    //     inRageMode = true;
    //     Sword.SetActive(false);
    //     //Sword2.SetActive(false);
    //     rageSowrd.SetActive(true);
    //     //rageSowrd2.SetActive(true);
    //     yield return new WaitForSeconds(LastingTime);
    //     rageSowrd.SetActive(false);
    //     //rageSowrd2.SetActive(false);
    //     Sword.SetActive(true);
    //     //Sword2.SetActive(true);
    //     inRageMode = false;
    // }
}