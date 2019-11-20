using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float duration = 0.65f;

    public bool moving;
    public bool falling;
    private float startTime;
    private float targetY;
    public float distance = 4f;

    private Animator AnimeC;
    private Vector3 OldPosition;

    // Start is called before the first frame update
    void Start()
    {
        AnimeC = GameObject.Find("Sword").GetComponent<Animator>();
        OldPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    JumpUpSwipe();
        //}

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    JumpDownSwipe();
        //}
    }

    public void JumpUpSwipe()
    {
        if (moving || transform.position.y > 1) return;

        moving = true;
        startTime = Time.time;
        targetY = transform.position.y + distance;
    }

    public void JumpDownSwipe()
    {
        moving = false;
        falling = false;

        gameObject.transform.position = new Vector3(OldPosition.x, OldPosition.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            var position = transform.position;
            MoveToTarget();

            if (Mathf.Approximately(transform.position.y, targetY))
            {
                //transform.position = new Vector3(position.x, targetY, position.z);

                targetY = position.y - distance;
                startTime = Time.time;
                moving = false;
                falling = true;
            }
        }

        if (falling)
        {
            var position = transform.position;
            MoveToTarget();

            if (Mathf.Approximately(transform.position.y, targetY))
            {
                transform.position = new Vector3(position.x, targetY, position.z);

                falling = false;
            }
        }
    }

    void MoveToTarget()
    {
        var t = (Time.time - startTime) / duration;

        var position = transform.position;
        var newY = Mathf.SmoothStep(position.y, targetY, t);
        position = new Vector3(position.x, newY, position.z);
        transform.position = position;
    }
}
