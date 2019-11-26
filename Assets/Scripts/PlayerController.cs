using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private GameObject PlayerCamera;

    //    private bool hasStashInput;
    private SwipeDirection stashedDirection;

    public TimeController TimeManager;
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
        PlayerCamera = GameObject.Find("Main Camera");
        OldPosition = gameObject.transform.position;
    }


    private void Update()
    {
//#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.A))
        {
            OnLeftSwipe();

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            OnRightSwipe();

        }

        if (Input.GetKeyDown(KeyCode.B))
        {

            if (WeaponDMG.instance.BulletTime)
            {
                WeaponDMG.instance.BulletTime = false;
            }
            else
            {
                WeaponDMG.instance.BulletTime = true;
            }
            TimeManager.BulletTime();
                       
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnUpSwipe();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            OnDownSwipe();
        }



        
//#endif
    }

    private void FixedUpdate()
    {

        if (LastCommand != PlayerCommand.idle)
        {
            if (Time.time - LastCommandTime >= 1.1f && Time.time - LastCommandTime <= 1.2f )
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

        if ((transform.position.z > OldPosition.z)) return;
        

        PlayerCamera.GetComponent<CameraShake>().CameraLeftSwipt();

        if (moving)
        {
            stashedDirection = SwipeDirection.Left;
            Invoke(nameof(ClearStash), 0.5f);
            return;
        }


        if (LastCommand == PlayerCommand.Rightswing)
        {
            AnimeC.SetBool("idle", false);
            AnimeC.ResetTrigger("RS");
            AnimeC.ResetTrigger("Left to Right");
            AnimeC.SetTrigger("Right to Left");
            //Debug.Log("R TO L");
        }
        else
        {
            AnimeC.SetBool("idle", false);
            AnimeC.ResetTrigger("Right to Left");
            AnimeC.ResetTrigger("LS");
            AnimeC.ResetTrigger("RS");
            AnimeC.SetTrigger("LS");

        }

        LastCommand = PlayerCommand.Leftswing;
        LastCommandTime = Time.time;
        moving = true;
        startTime = Time.time;
        targetZ = transform.position.z + distance;
        
    }

    public void OnRightSwipe()
    {
        //Debug.Log(transform.position.z);
        if (transform.position.z < -distance) return;
        PlayerCamera.GetComponent<CameraShake>().CameraRightSwipe();

        if (moving)
        {
            stashedDirection = SwipeDirection.Right;
            Invoke(nameof(ClearStash), 0.5f);
            return;
        }
        if (LastCommand == PlayerCommand.Leftswing)
        {
            AnimeC.SetBool("idle", false);
            AnimeC.ResetTrigger("LS");
            AnimeC.ResetTrigger("Right to Left");
            AnimeC.SetTrigger("Left to Right");
        }
        else
        {
            AnimeC.SetBool("idle", false);
            AnimeC.ResetTrigger("Left to Right");
            AnimeC.ResetTrigger("RS");
            AnimeC.ResetTrigger("LS");
            AnimeC.SetTrigger("RS");

        }

        LastCommand = PlayerCommand.Rightswing;
        LastCommandTime = Time.time;
        moving = true;
        startTime = Time.time;
        targetZ = transform.position.z - distance;
    }

    public void OnDownSwipe()
    {
        if (slashing || gameObject.transform.position.y < 1.0f)
            return;

        PlayerCamera.GetComponent<CameraShake>().CameraDownSwipe();
        gameObject.GetComponent<PlayerJump>().JumpDownSwipe();

        gameObject.transform.position = new Vector3(OldPosition.x, OldPosition.y, transform.position.z);

        AnimeC.SetTrigger("DS");

        slashing = true;
        startTime = Time.time;
        targetZ = transform.position.z;

        StartCoroutine(WaitForSlash());
    }

    public void OnUpSwipe()
    {
        gameObject.GetComponent<PlayerJump>().JumpUpSwipe();
    }

    IEnumerator WaitForSlash()
    {
        yield return new WaitForSeconds(0.25f);
        slashing = false;
    }

}
