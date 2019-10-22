using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float RotateAngle = 5;
    public float RotateSpeed = 10;
    public float RotateBackSpeed = 15;
    PlayerController Character;

    public float shakeAmount = 1;//The amount to shake this frame.
    public float shakeDuration = 1;//The duration this frame.

    //Readonly values...
    float shakePercentage;//A percentage (0-1) representing the amount of shake to be applied when setting rotation.
    float startAmount;//The initial shake amount (to determine percentage), set when ShakeCamera is called.
    float startDuration;//The initial shake duration, set when ShakeCamera is called.

    bool isRunning = false; //Is the coroutine running right now?

    public bool smooth = true;//Smooth rotation?
    public float smoothAmount = 5f;//Amount to smooth

    // Start is called before the first frame update
    void Start()
    {
        Character = GameObject.Find("Character").GetComponent<PlayerController>();
        ShakeCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !Character.moving)
        {
            StopAllCoroutines();
            isRunning = false;

            gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
            StartCoroutine(Rotate(RotateAngle, RotateSpeed, RotateBackSpeed));
        }

        if (Input.GetKeyDown(KeyCode.D) && !Character.moving)
        {
            StopAllCoroutines();
            isRunning = false;

            gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
            StartCoroutine(Rotate(-RotateAngle, RotateSpeed, RotateBackSpeed));
        }

        ShakeCamera();
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

    IEnumerator Rotate(float Angle, float RotateSpeed, float RotateBackSpeed)
    {
        while (gameObject.transform.rotation != Quaternion.Euler(0, 90, Angle))
        {
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(0, 90, Angle), 1 / RotateSpeed);
        }
        yield return new WaitForSeconds(1 / RotateSpeed);

        while (gameObject.transform.rotation != Quaternion.Euler(0, 90, 0))
        {
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(0, 90, 0), 1 / RotateBackSpeed);
            yield return null;
        }

        gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        yield return null;
    }
}
