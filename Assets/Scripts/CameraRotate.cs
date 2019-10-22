using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float RotateAngle = 5;
    public float RotateSpeed = 10;
    public float RotateBackSpeed = 15;
    PlayerController Character;

    // Start is called before the first frame update
    void Start()
    {
        Character = GameObject.Find("Character").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)&& !Character.moving)
        {
            StopAllCoroutines();
            gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
            StartCoroutine(RotateImage(RotateAngle, RotateSpeed, RotateBackSpeed));
        }

        if (Input.GetKeyDown(KeyCode.D)&&!Character.moving)
        {
            StopAllCoroutines();
            gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
            StartCoroutine(RotateImage(-RotateAngle, RotateSpeed, RotateBackSpeed));
        }

    }

    IEnumerator RotateImage(float Angle, float RotateSpeed, float RotateBackSpeed)
    {
        while (gameObject.transform.rotation != Quaternion.Euler(0, 90, Angle))
        {
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(0, 90, Angle), 1/RotateSpeed);
            yield return null;
        }

        while (gameObject.transform.rotation != Quaternion.Euler(0, 90, 0))
        {
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(0, 90, 0), 1/RotateBackSpeed);
            yield return null;
        }

        gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        yield return null;
    }
}
