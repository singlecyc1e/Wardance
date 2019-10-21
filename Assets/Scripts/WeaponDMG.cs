using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDMG : MonoBehaviour
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
        //if "move" in playercontroller is True
        if (other.gameObject.tag == "Enemy") {
            Destroy(other.gameObject.GetComponent<MeshRenderer>());
            other.gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
            Destroy(other.gameObject,1);// destroy the Enemy and play destroy deconstruction animation;

        } 
        //if "move" in playercontroller is False
        //gameover, pause the game
    }
}
