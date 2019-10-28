using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDMG : MonoBehaviour
{
    PlayerController Charac;
    RageSystem m_Rage;
    // Start is called before the first frame update
    void Start()
    {
        Charac = GameObject.Find("Character").GetComponent<PlayerController>();
        m_Rage = GameObject.Find("Character").GetComponent<RageSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if "move" in playercontroller is True

        if (Charac.moving|m_Rage.RageState) {
            if (other.gameObject.tag == "Enemy")
            {
                Destroy(other.gameObject.GetComponent<MeshRenderer>());
                other.gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
                Destroy(other.gameObject, 3);// destroy the Enemy and play destroy deconstruction animation;
                m_Rage.AddRageValue();
            }       
        }
        else
        {
            if (other.gameObject.tag == "Enemy")
            {
                //pause game
                Time.timeScale = 0;
                GameObject.Find("Death Menu").transform.GetChild(0).gameObject.SetActive(true);
            

            }
        }
        //if "move" in playercontroller is False
        //gameover, pause the game
    }
}
