using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float slowfactor = .05f;
    public float slowduration = 2f;

    private void Update()
    {
        if (WeaponDMG.instance.Alive)
        {
            Time.timeScale += (1 / slowduration) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        }

    }

    public void BulletTime()
    {
        Time.timeScale = slowfactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
}
