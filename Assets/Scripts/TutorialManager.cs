using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private bool Sng0 = true;
    private bool Sng1 = true;
    private bool Sng2 = true;
    private bool Sng2_2 = true;
    private bool Sng2_3 = true;
    private bool Sng3 = true;
    private bool Sng4 = true;
    public Animator hand;
    public Image rightimg;
    public Image leftimg;
    public Image upimg;


    // Start is called before the first frame update
    void Start()
    {
        PlayerController.instance.enabled = false;
        hand.gameObject.SetActive(false);
        rightimg.gameObject.SetActive(false);
        leftimg.gameObject.SetActive(false);
        upimg.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(Time.timeSinceLevelLoad);
        if ((Time.timeSinceLevelLoad) >= 4.5f && Sng1)
        {
            rightimg.gameObject.SetActive(true);
            hand.gameObject.SetActive(true);
            hand.SetTrigger("right");
            Sng1 = false;
            TimeController.instance.BulletTime();
        }

        if ((Time.timeSinceLevelLoad) >= 4.55f && Sng0)
        {
            PlayerController.instance.enabled = true;
            Sng0 = false;
        }

        if ((Time.timeSinceLevelLoad) >= 5.0f && Sng2_3)
        {

            TimeController.instance.backToNormal = true;
            Sng2_3 = false;
            rightimg.gameObject.SetActive(false);
            hand.gameObject.SetActive(false);
            PlayerController.instance.enabled = false;
        }



        if ((Time.timeSinceLevelLoad) >= 5.5f && Sng2_2)
        {
            PlayerController.instance.enabled = true;
            Sng2_2 = false;
            rightimg.gameObject.SetActive(false);
            leftimg.gameObject.SetActive(true);
            hand.gameObject.SetActive(true);
            hand.SetTrigger("left");
            TimeController.instance.BulletTime();
        }

        if ((Time.timeSinceLevelLoad) >= 6f && Sng2) {
            TimeController.instance.backToNormal = true;
            Sng2 = false;
            hand.gameObject.SetActive(false);
            leftimg.gameObject.SetActive(false);
            PlayerController.instance.enabled = false;
        }

        if ((Time.timeSinceLevelLoad) >= 8f && Sng3)
        {
            PlayerController.instance.enabled = true;
            TimeController.instance.BulletTime();
            Sng3 = false;
            hand.gameObject.SetActive(true);
            upimg.gameObject.SetActive(true);
            hand.SetTrigger("up");
        }

        if ((Time.timeSinceLevelLoad) >= 8.6f && Sng4)
        {
            TimeController.instance.backToNormal = true;
            Sng4 = false;
            hand.gameObject.SetActive(false);
            upimg.gameObject.SetActive(false);
        }
    }
}
