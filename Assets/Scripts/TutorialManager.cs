using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private float time;
    private bool Sng0 = true;
    private bool Sng1 = true;
    private bool Sng2 = true;
    private bool Sng3 = true;
    private bool Sng4 = true;

    // Start is called before the first frame update
    void Start()
    {
        time = Time.time;
        PlayerController.instance.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if ((Time.time - time) >= 5.1f && Sng1)
        {
            Sng1 = false;
            TimeController.instance.BulletTime();
        }

        if ((Time.time - time) >= 5.18f && Sng0)
        {
            PlayerController.instance.enabled = true;
            Sng0 = false;
        }

        if ((Time.time - time) >= 6.3f && Sng2) {
            TimeController.instance.backToNormal = true;
            Sng2 = false;
        }

        if ((Time.time - time) >= 8.9f && Sng3)
        {
            TimeController.instance.BulletTime();
            Sng3 = false;
        }

        if ((Time.time - time) >= 9.45f && Sng4)
        {
            TimeController.instance.backToNormal = true;
            Sng4 = false;
        }
    }
}
