using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private float time;
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
        time = Time.time;
        PlayerController.instance.enabled = false;
        hand.gameObject.SetActive(false);
        rightimg.gameObject.SetActive(false);
        leftimg.gameObject.SetActive(false);
        upimg.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if ((Time.time - time) >= 5.1f && Sng1)
        {
            rightimg.gameObject.SetActive(true);
            hand.gameObject.SetActive(true);
            hand.SetTrigger("right");
            Sng1 = false;
            TimeController.instance.BulletTime();
        }

        if ((Time.time - time) >= 5.18f && Sng0)
        {
            PlayerController.instance.enabled = true;
            Sng0 = false;
        }

        if ((Time.time - time) >= 5.55f && Sng2_3)
        {

            TimeController.instance.backToNormal = true;
            Sng2_3 = false;
            rightimg.gameObject.SetActive(false);
            hand.gameObject.SetActive(false);
        }



        if ((Time.time - time) >= 6.05f && Sng2_2)
        {
            PlayerController.instance.enabled = true;
            Sng2_2 = false;
            rightimg.gameObject.SetActive(false);
            leftimg.gameObject.SetActive(true);
            hand.gameObject.SetActive(true);
            hand.SetTrigger("left");
            TimeController.instance.BulletTime();
        }

        if ((Time.time - time) >= 6.5f && Sng2) {
            TimeController.instance.backToNormal = true;
            Sng2 = false;
            hand.gameObject.SetActive(false);
            leftimg.gameObject.SetActive(false);
        }

        if ((Time.time - time) >= 8.9f && Sng3)
        {
            TimeController.instance.BulletTime();
            Sng3 = false;
            hand.gameObject.SetActive(true);
            upimg.gameObject.SetActive(true);
            hand.SetTrigger("up");
        }

        if ((Time.time - time) >= 9.45f && Sng4)
        {
            TimeController.instance.backToNormal = true;
            Sng4 = false;
            hand.gameObject.SetActive(false);
            upimg.gameObject.SetActive(false);
        }
    }
}
