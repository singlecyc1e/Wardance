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

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Update()
    {
        if (WeaponDMG.instance.Alive && backToNormal)
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

}
