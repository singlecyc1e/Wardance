using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

public enum SwipeDirection {
    None,
    Left,
    Right
}

enum PlayerCommand {
    idle = 0,
    Leftswing = 1,
    Rightswing = 2
}

public enum Direction {
    None,
    Up,
    Left,
    Down,
    Right
}

public struct FingerStorage {
    public Direction direction;
    // public int counter;
    public float startTime;
    public float threshold;

    public FingerStorage(Direction direction, float newThreshold) {
        this.direction = direction;
        // this.counter = counter;
        startTime = Time.time * 1000;
        threshold = newThreshold;
    }

    // public void DecreaseCounter() {
    //     --counter;
    // }

    public bool Check() {
        return Time.time * 1000 - startTime > threshold;
    }

    public void Reset() {
        startTime = Time.time;
    }
}

public class PlayerController : MonoBehaviour {
    public float duration;

    public bool moving;
    public bool slashing;

    private float startTime;
    private float targetZ;
    private Animator AnimeC;
    public float distance = 4f;
    public bool idleTimeup;

    public AudioClip[] SwipeSound;

    private AudioSource audiosource;
    private PlayerCommand LastCommand = PlayerCommand.idle;
    private float LastCommandTime = 0f;

    private Vector3 OldPosition;
    private GameObject PlayerCamera;

    //    private bool hasStashInput;
    private SwipeDirection stashedDirection;

    // private Dictionary<int, FingerStorage> storedTouches;
    private FingerStorage fingerStorage;
    private const float fingerThreshold = 100;

    public TimeController TimeManager;
    public static PlayerController instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        AnimeC = GameObject.Find("Sword").GetComponent<Animator>();
        PlayerCamera = GameObject.Find("Main Camera");
        OldPosition = gameObject.transform.position;
        audiosource = GetComponent<AudioSource>();
        
        fingerStorage = new FingerStorage(Direction.None, fingerThreshold);
    }

    private void UpdateTouch() {
        if(LeanTouch.Fingers.Count == 0) return;
        
        var deltaPosition = Lean.Touch.LeanGesture.GetScreenDelta();
        var angle = Vector2.SignedAngle(Vector2.up, deltaPosition);
        var directionDetected = Direction.None;
        if (angle >= -45 && angle < 45) {
            directionDetected = Direction.Up;
        } else if (angle >= 45 && angle < 135) {
            directionDetected = Direction.Right;
        } else if (angle >= 135 || angle < -135) {
            directionDetected = Direction.Down;
        } else if (angle >= -135 && angle < -45) {
            directionDetected = Direction.Left;
        }
        
        Debug.Log("Detect: " + directionDetected);

        // switch (directionDetected == fingerStorage.direction) {
        //     case true:
        //         fingerStorage.DecreaseCounter();
        //         break;
        //     default:
        //         fingerStorage = new FingerStorage(directionDetected, 5);
        //         break;
        // }

        if (directionDetected != fingerStorage.direction) {
            fingerStorage = new FingerStorage(directionDetected, fingerThreshold);
        }

        if (fingerStorage.Check()) {
            switch (fingerStorage.direction) {
                case Direction.None:
                    break;
                case Direction.Up:
                    OnUpSwipe();
                    break;
                case Direction.Left:
                    OnRightSwipe();
                    break;
                case Direction.Down:
                    OnDownSwipe();
                    break;
                case Direction.Right:
                    OnLeftSwipe();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            fingerStorage.Reset();
        }
    }


    private void Update() {
#if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.A)) {
            OnLeftSwipe();
        } else if (Input.GetKeyDown(KeyCode.D)) {
            OnRightSwipe();
        }

        if (Input.GetKeyDown(KeyCode.B)) {
            if (WeaponDMG.instance.BulletTime) {
                WeaponDMG.instance.BulletTime = false;
            } else {
                WeaponDMG.instance.BulletTime = true;
            }

            TimeManager.BulletTime();
        }


        if (Input.GetKeyDown(KeyCode.Space)) {
            OnUpSwipe();
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            OnDownSwipe();
        }
#endif
        
        UpdateTouch();
    }

    private void FixedUpdate() {
        if (LastCommand != PlayerCommand.idle) {
            if (Time.time - LastCommandTime >= 1.1f && Time.time - LastCommandTime <= 1.2f) {
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

    private void ClearStash() {
        stashedDirection = SwipeDirection.None;
    }

    public void OnLeftSwipe() {
        if ((transform.position.z > OldPosition.z)) return;

        audiosource.clip = SwipeSound[UnityEngine.Random.Range(0, 3)];
        audiosource.Play();
        PlayerCamera.GetComponent<CameraShake>().CameraLeftSwipt();

        if (moving) {
            stashedDirection = SwipeDirection.Left;
            Invoke(nameof(ClearStash), 0.5f);
            return;
        }


        if (LastCommand == PlayerCommand.Rightswing) {
            AnimeC.SetBool("idle", false);
            AnimeC.ResetTrigger("RS");
            AnimeC.ResetTrigger("Left to Right");
            AnimeC.SetTrigger("Right to Left");
            //Debug.Log("R TO L");
        } else {
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

    public void OnRightSwipe() {
        //Debug.Log(transform.position.z);
        if (transform.position.z < -distance) return;

        audiosource.clip = SwipeSound[UnityEngine.Random.Range(0, 3)];
        audiosource.Play();
        PlayerCamera.GetComponent<CameraShake>().CameraRightSwipe();

        if (moving) {
            stashedDirection = SwipeDirection.Right;
            Invoke(nameof(ClearStash), 0.5f);
            return;
        }

        if (LastCommand == PlayerCommand.Leftswing) {
            AnimeC.SetBool("idle", false);
            AnimeC.ResetTrigger("LS");
            AnimeC.ResetTrigger("Right to Left");
            AnimeC.SetTrigger("Left to Right");
        } else {
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

    public void OnDownSwipe() {
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

    public void OnUpSwipe() {
        gameObject.GetComponent<PlayerJump>().JumpUpSwipe();
    }

    IEnumerator WaitForSlash() {
        yield return new WaitForSeconds(0.25f);
        slashing = false;
    }

    public IEnumerator ShakeBody(float duration, float magnitude) {
        Debug.Log("shake");
        Vector3 originalPos = transform.position;
        float elapsed = 0.0f;
        while (elapsed < duration) {
            float z = UnityEngine.Random.Range(-1f, 1f) * magnitude;
            float y = UnityEngine.Random.Range(0, 1f) * magnitude;
            transform.localPosition = new Vector3(originalPos.x, y, z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}