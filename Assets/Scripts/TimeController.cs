using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float slowfactor = .05f;
    public float slowduration = 2f;
    public bool backToNormal = false;
    public static TimeController instance;
    private float tempTime;
    public GameObject puaseOBJ;
    public GameObject ContinueOBJ;
    private bool puasestate = false;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    private void Start()
    {
        puaseOBJ.SetActive(true);
        ContinueOBJ.SetActive(false);
    }
    private void Update()
    {
        if (WeaponDMG.instance.Alive && backToNormal && !puasestate)
        {
            Debug.Log("here");
            Time.timeScale += (1 / slowduration) * Time.unscaledDeltaTime;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        }
    }

    public void BulletTime()
    {
        backToNormal = false;
        Time.timeScale = slowfactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    public void Pause()
    {
        tempTime = Time.timeScale;
        Time.timeScale = 0;
        puaseOBJ.SetActive(false);
        ContinueOBJ.SetActive(true);
        puasestate = true;
    }

    public void Continue()
    {
        Time.timeScale = tempTime;
        puaseOBJ.SetActive(true);
        ContinueOBJ.SetActive(false);
        puasestate = false;
    }

}
