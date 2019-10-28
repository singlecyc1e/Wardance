﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float ShakeAngle = 0.3f;
    public float ShakeSpeed = 1;

    public float SlashAngle = 10;
    public float SlashSpeed = 1.5f;

    PlayerController Character;

    bool isRotating = false;

    //Shake public variables
    float shakeAmount = 1;//The amount to shake this frame.
    float shakeDuration = 1;//The duration this frame.
    bool smooth = true;//Smooth rotation?
    float smoothAmount = 5f;//Amount to smooth

    //Shake readonly values...
    float shakePercentage;//A percentage (0-1) representing the amount of shake to be applied when setting rotation.
    float startAmount;//The initial shake amount (to determine percentage), set when ShakeCamera is called.
    float startDuration;//The initial shake duration, set when ShakeCamera is called.
    bool isRunning = false; //Is the coroutine running right now?

    // Start is called before the first frame update
    void Start()
    {
        Character = GameObject.Find("Character").GetComponent<PlayerController>();
        //ShakeCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !Character.moving)
        {
            StopAllCoroutines();
            isRunning = false;

            gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
            StartCoroutine(Rotate(SlashAngle, SlashSpeed / 10, false));
        }

        if (Input.GetKeyDown(KeyCode.D) && !Character.moving)
        {
            StopAllCoroutines();
            isRunning = false;

            gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
            StartCoroutine(Rotate(-SlashAngle, SlashSpeed / 5, false));
        }

        if(!isRotating)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
            StartCoroutine(Rotate(ShakeAngle, ShakeSpeed / 10, true));
        }
    }

    void ShakeCamera()
    {
        startAmount = shakeAmount;//Set default (start) values
        startDuration = shakeDuration;//Set default (start) values

        if (!isRunning)
            StartCoroutine(Shake());//Only call the coroutine if it isn't currently running. Otherwise, just set the variables.
    }

    IEnumerator Shake()
    {
        isRunning = true;

        while (shakeDuration >= 0)
        {
            Vector3 rotationAmount = Random.insideUnitSphere * shakeAmount;
            rotationAmount.z = 0;

            shakePercentage = shakeDuration / startDuration;

            shakeAmount = startAmount * shakePercentage;
            shakeDuration = Mathf.Lerp(shakeDuration, 0, Time.deltaTime);


            if (smooth)
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotationAmount), Time.deltaTime * smoothAmount);
            else
                transform.localRotation = Quaternion.Euler(rotationAmount);

            yield return null;
        }
        transform.localRotation = Quaternion.identity;
        isRunning = false;
    }

    IEnumerator Rotate(float Angle, float RotateSpeed, bool ShakeMode)
    {
        isRotating = true;
        while (gameObject.transform.rotation != Quaternion.Euler(0, 90, Angle))
        {
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(0, 90, Angle), RotateSpeed);
            yield return null;
        }
        //yield return new WaitForSeconds(1 / RotateSpeed);

        while (gameObject.transform.rotation != Quaternion.Euler(0, 90, 0))
        {
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(0, 90, 0), RotateSpeed);
            yield return null;
        }

        if (ShakeMode)
        {
            while (gameObject.transform.rotation != Quaternion.Euler(0, 90, -Angle))
            {
                gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(0, 90, -Angle), RotateSpeed);
                yield return null;
            }

            while (gameObject.transform.rotation != Quaternion.Euler(0, 90, 0))
            {
                gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(0, 90, 0), RotateSpeed);
                yield return null;
            }
        }

        gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        isRotating = false;
        yield return null;
    }
}
