using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearEnemy : MonoBehaviour
{
    public Animator SpearAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "HintTrigger")
        {
            SpearAnimator.SetTrigger("Start");
        }
    }
}
