using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum SwipeDirection
{
    None, Left, Right
}
enum PlayerCommand
{
    idle = 0,
    Leftswing = 1,
    Rightswing = 2
}
public class PlayerController : MonoBehaviour
{
    public float duration;

    public bool moving;
    public bool slashing;
    private float startTime;
    private float targetZ;
    private Animator AnimeC;
    public float distance = 4f;
    public bool idleTimeup;

    private PlayerCommand LastCommand = PlayerCommand.idle;
    private float LastCommandTime = 0f;

    private Vector3 OldPosition;

    //    private bool hasStashInput;
    private SwipeDirection stashedDirection;

    public static PlayerController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        AnimeC = GameObject.Find("Sword").GetComponent<Animator>();
        OldPosition = gameObject.transform.position;
    }


    private void Update()
    {
#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.A))
        {
            OnLeftSwipe();

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            OnRightSwipe();

        }

//<<<<<<< Updated upstream
//        if (Input.GetKeyDown(KeyCode.S) && gameObject.transform.position.y > 1)
//        {
//            OnDownSwipe();
//        }
//=======
//>>>>>>> Stashed changes
#endif
    }

    private void FixedUpdate()
    {

        if (LastCommand != PlayerCommand.idle)
        {
            if (Time.time - LastCommandTime > 1f)
            {
                AnimeC.ResetTrigger("Left to Right");
                AnimeC.ResetTrigger("Right to Left");
                AnimeC.ResetTrigger("LS");
                AnimeC.ResetTrigger("RS");
                AnimeC.SetBool("idle", true);
                LastCommand = PlayerCommand.idle;
            }
        }

        if (!moving) return;

        var t = (Time.time - startTime) / duration;
        var position = transform.position;
        var newZ = Mathf.SmoothStep(position.z, targetZ, t);
        position = new Vector3(position.x, position.y, newZ);
        transform.position = position;

        if (Mathf.Approximately(transform.position.z, targetZ)) {
            transform.position = new Vector3(position.x, position.y, targetZ);
            moving = false;
            switch (stashedDirection) {
                case SwipeDirection.None:
                    return;
                    break;
                case SwipeDirection.Left:
                    OnLeftSwipe();
                    break;
                case SwipeDirection.Right:
                    OnRightSwipe();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


    }

    private void ClearStash()
    {
        stashedDirection = SwipeDirection.None;
    }

    public void OnLeftSwipe()
    {
        if (!(transform.position.z < distance)) return;

        if (moving)
        {
            stashedDirection = SwipeDirection.Left;
            Invoke(nameof(ClearStash), 0.5f);
            //StopCoroutine(IdleStateTimer());
            return;
        }

        //StopCoroutine(IdleStateTimer());
        if (LastCommand == PlayerCommand.Rightswing)
        {
            AnimeC.SetBool("idle", false);
            AnimeC.ResetTrigger("RS");
            AnimeC.ResetTrigger("Left to Right");
            AnimeC.SetTrigger("Right to Left");
            SoundEvent.a.slash.Invoke();
            //Debug.Log("R TO L");
        }
        else
        {
            AnimeC.SetBool("idle", false);
            AnimeC.ResetTrigger("Right to Left");
            AnimeC.ResetTrigger("LS");
            AnimeC.ResetTrigger("RS");
            AnimeC.SetTrigger("LS");
            SoundEvent.a.slash.Invoke();

        }

        LastCommand = PlayerCommand.Leftswing;
        LastCommandTime = Time.time;
        moving = true;
        startTime = Time.time;
        targetZ = transform.position.z + distance;
        //StartCoroutine(IdleStateTimer());
    }

    public void OnRightSwipe()
    {
        if (!(transform.position.z > -distance)) return;

        if (moving)
        {
            stashedDirection = SwipeDirection.Right;
            Invoke(nameof(ClearStash), 0.5f);
            //StopCoroutine(IdleStateTimer());
            return;
        }
        //StopCoroutine(IdleStateTimer());
        if (LastCommand == PlayerCommand.Leftswing)
        {
            AnimeC.SetBool("idle", false);
            AnimeC.ResetTrigger("LS");
            AnimeC.ResetTrigger("Right to Left");
            AnimeC.SetTrigger("Left to Right");
            SoundEvent.a.slash.Invoke();
            //Debug.Log("R TO L");
        }
        else
        {
            AnimeC.SetBool("idle", false);
            AnimeC.ResetTrigger("Left to Right");
            AnimeC.ResetTrigger("RS");
            AnimeC.ResetTrigger("LS");
            AnimeC.SetTrigger("RS");
            SoundEvent.a.slash.Invoke();

        }

        LastCommand = PlayerCommand.Rightswing;
        LastCommandTime = Time.time;
        moving = true;
        startTime = Time.time;
        targetZ = transform.position.z - distance;
    }
//<<<<<<< Updated upstream

//    public void OnDownSwipe()
//    {
//        if (slashing)
//            return;

//        gameObject.transform.position = new Vector3(OldPosition.x, OldPosition.y, transform.position.z);

//        AnimeC.ResetTrigger("RS");
//        AnimeC.ResetTrigger("LS");
//        AnimeC.SetTrigger("DS");

//        slashing = true;
//        startTime = Time.time;
//        targetZ = transform.position.z;

//        StartCoroutine(WaitForSlash());
//    }

//    IEnumerator WaitForSlash()
//    {
//        yield return new WaitForSeconds(0.25f);
//        slashing = false;
//    }

}
