using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timedisplay : MonoBehaviour
{
    private Text mytime;
    private void Start()
    {
        mytime = this.transform.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        mytime.text = Time.time.ToString();
    }
}
