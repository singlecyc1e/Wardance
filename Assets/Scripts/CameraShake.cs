using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
            StartCoroutine(RotateImage(5, 0.1f));
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
            StartCoroutine(RotateImage(-5, 0.1f));
        }
    
    }

    IEnumerator RotateImage(float Angle, float moveSpeed)
    {
        while (gameObject.transform.rotation != Quaternion.Euler(0, 90, Angle))
        {
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(0, 90, Angle), moveSpeed);
            yield return null;
        }

        while (gameObject.transform.rotation != Quaternion.Euler(0, 90, 0))
        {
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(0, 90, 0), 0.2f);
            yield return null;
        }

        gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        yield return null;
    }
}
