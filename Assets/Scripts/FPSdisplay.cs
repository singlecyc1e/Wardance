using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSdisplay : MonoBehaviour
{
    private Text myfps;
    private void Start()
    {
        myfps = this.transform.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        myfps.text = (1f / Time.deltaTime).ToString();
    }
}
