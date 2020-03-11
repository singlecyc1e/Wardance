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
    private bool Sng5 = true;
    private bool Sng6 = true;
    public Animator hand;
    public Image rightimg;
    public Image leftimg;
    public Image upimg;
    private Vector3 handposition;

    // Start is called before the first frame update
    void Start()
    {
        
        time = 0;
        PlayerController.instance.enabled = false;
        PlayerJump.instance.enabled = false;
        hand.gameObject.SetActive(false);
        rightimg.gameObject.SetActive(false);
        leftimg.gameObject.SetActive(false);
        upimg.gameObject.SetActive(false);
        handposition = hand.gameObject.GetComponent<Transform>().localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        
        if ((time) >= 5.2f && Sng1)
        {
            rightimg.gameObject.SetActive(true);
            hand.gameObject.SetActive(true);
            hand.SetTrigger("right");
            Sng1 = false;
            TimeController.instance.BulletTime();
        }

        if ((time) >= 5.25f && Sng0)
        {
            PlayerController.instance.enabled = true;
            Sng0 = false;
            
        }

        if ((time) >= 5.65f && Sng2_3)
        {

            TimeController.instance.backToNormal = true;
            Sng2_3 = false;
            
            rightimg.gameObject.SetActive(false);
            hand.gameObject.SetActive(false);
            PlayerController.instance.enabled = false;
        }



        if ((time) >= 6.2f && Sng2_2)
        {
            PlayerController.instance.enabled = true;
            Sng2_2 = false;
            rightimg.gameObject.SetActive(false);
            leftimg.gameObject.SetActive(true);
            hand.gameObject.SetActive(true);
            //hand.gameObject.GetComponent<Transform>().localPosition = handposition; 
            hand.SetTrigger("left");
            TimeController.instance.BulletTime();
        }

        if ((time) >= 6.6f && Sng2) {
            TimeController.instance.backToNormal = true;
            Sng2 = false;
            hand.gameObject.SetActive(false);
            leftimg.gameObject.SetActive(false);
            PlayerController.instance.enabled = false;
        }

        if ((time) >= 8.6f && Sng3)
        {
            PlayerJump.instance.enabled = true;
            PlayerController.instance.enabled = true;
            TimeController.instance.BulletTime();
            Sng3 = false;
            hand.gameObject.SetActive(true);
            upimg.gameObject.SetActive(true);
            //hand.GetComponent<RectTransform>().localPosition = new Vector3(-30,0,0);
            hand.SetTrigger("up");

        }

        if ((time) >= 8.95f && Sng4)
        {
            TimeController.instance.backToNormal = true;
            Sng4 = false;
            hand.gameObject.SetActive(false);
            upimg.gameObject.SetActive(false);
        }

        if ((time) >= 10f && Sng5)
        {
            
            hand.gameObject.SetActive(true);
            //hand.gameObject.GetComponent<Transform>().localPosition = new Vector3(30.1f, 185.9f, 0f);
            hand.SetTrigger("click");
            TimeController.instance.BulletTime();
            Sng5 = false;
        }

        if ((time) >= 10.7f && Sng6)
        {
            TimeController.instance.backToNormal = true;
            hand.gameObject.SetActive(false);
            Sng6 = false;
        }

        if (time > 20f)
        {
            TimeController.instance.BulletTime();
            GameObject.Find("MenuManager").GetComponent<MenuManager>().LoadNextLevel();
            PlayerPrefs.SetInt(LevelController.finishTutorialKey, 1);
        }


    }
}
