using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintTrigger : MonoBehaviour
{
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
        if(other.gameObject.tag == "Enemy")
        {
            var HintEffectParticle = other.gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();

            if (!HintEffectParticle.isPlaying)
            {
                HintEffectParticle.Play();
            }

            Debug.Log(other.gameObject.transform.GetChild(1).name);
        }
    }
}
