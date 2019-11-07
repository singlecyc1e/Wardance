using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwipeDirection
{
    None, Left, Right
}
<<<<<<< HEAD
enum PlayerCommand
{
    idle = 0,
    Leftswing = 1,
    Rightswing = 2
}
public class PlayerController : MonoBehaviour
{
=======

public class PlayerController : MonoBehaviour {
>>>>>>> brian_move
    public float duration;

    public bool moving;
    public bool slashing;
    private float startTime;
    private float targetZ;
    private Animator AnimeC;
    public float distance = 4f;

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
<<<<<<< HEAD

        if (Input.GetKeyDown(KeyCode.A))
        {
            OnLeftSwipe();

        }
        else if (Input.GetKeyDown(KeyCode.D))
=======
        if (Input.GetKeyDown(KeyCode.A)) {
            OnLeftSwipe();
        } else if (Input.GetKeyDown(KeyCode.D)) {
            OnRightSwipe();
        }

        if (Input.GetKeyDown(KeyCode.S) && gameObject.transform.position.y > 1)
>>>>>>> brian_move
        {
            OnRightSwipe();

        }
<<<<<<< HEAD


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
=======
#endif
    }

    private void FixedUpdate() {
        if (moving)
        {
            var t = (Time.time - startTime) / duration;
            var position = transform.position;
            var newZ = Mathf.SmoothStep(position.z, targetZ, t);
            position = new Vector3(position.x, position.y, newZ);
            transform.position = position;

            if (Mathf.Approximately(transform.position.z, targetZ))
            {
                transform.position = new Vector3(position.x, position.y, targetZ);
                moving = false;
                switch (stashedDirection)
                {
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

>>>>>>> brian_move
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
<<<<<<< HEAD

        if (moving)
        {
=======
        if (moving) {
>>>>>>> brian_move
            stashedDirection = SwipeDirection.Left;
            Invoke(nameof(ClearStash), 0.1f);
            return;
        }
        
        AnimeC.ResetTrigger("LS");
        AnimeC.ResetTrigger("RS");
        AnimeC.SetTrigger("LS");

<<<<<<< HEAD
        //StopCoroutine(IdleStateTimer());
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
=======
>>>>>>> brian_move
        moving = true;
        startTime = Time.time;
        targetZ = transform.position.z + distance;
    }

    public void OnRightSwipe()
    {
        if (!(transform.position.z > -distance)) return;
<<<<<<< HEAD

        if (moving)
        {
=======
        if (moving) {
>>>>>>> brian_move
            stashedDirection = SwipeDirection.Right;
            Invoke(nameof(ClearStash), 0.1f);
            return;
        }
<<<<<<< HEAD
        //StopCoroutine(IdleStateTimer());
        if (LastCommand == PlayerCommand.Leftswing)
        {
            AnimeC.SetBool("idle", false);
            AnimeC.ResetTrigger("LS");
            AnimeC.ResetTrigger("Right to Left");
            AnimeC.SetTrigger("Left to Right");
            //Debug.Log("R TO L");
        }
        else
        {
            AnimeC.SetBool("idle", false);
            AnimeC.ResetTrigger("Left to Right");
            AnimeC.ResetTrigger("RS");
            AnimeC.ResetTrigger("LS");
            AnimeC.SetTrigger("RS");

        }
=======
        
        AnimeC.ResetTrigger("RS");
        AnimeC.ResetTrigger("LS");
        AnimeC.SetTrigger("RS");
>>>>>>> brian_move

        moving = true;
        startTime = Time.time;
        targetZ = transform.position.z - distance;
    }

    //public void OnDownSwipe()
    //{
    //    if (slashing)
    //        return;

    //    gameObject.transform.position = new Vector3(OldPosition.x, OldPosition.y, transform.position.z);

    //    AnimeC.ResetTrigger("RS");
    //    AnimeC.ResetTrigger("LS");
    //    AnimeC.SetTrigger("DS");

    //    slashing = true;
    //    startTime = Time.time;
    //    targetZ = transform.position.z;

    //    StartCoroutine(WaitForSlash());
    //}

<<<<<<< HEAD
    //IEnumerator WaitForSlash()
    //{
    //    yield return new WaitForSeconds(0.25f);
    //    slashing = false;
    //}
=======
    IEnumerator WaitForSlash()
    {
        yield return new WaitForSeconds(0.25f);
        slashing = false;
    }
>>>>>>> brian_move
}
