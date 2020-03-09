using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resultsound : MonoBehaviour
{
    private AudioSource mysource;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(coroutineA());
    }

    IEnumerator coroutineA()
    {
        // wait for 1 second
        Debug.Log("coroutineA created");
        yield return new WaitForSeconds(0.1f);
        mysource.Play();
        yield return StartCoroutine(coroutineB());
        Debug.Log("coroutineA running again");
    }

    IEnumerator coroutineB()
    {
        // wait for 1 second
        Debug.Log("coroutineA created");
        yield return new WaitForSeconds(0.1f);
        mysource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
