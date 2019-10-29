using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDMG : MonoBehaviour
{
    public Text UI_killscore; 
    public float killscore = 0;
    public static WeaponDMG instance;
    // Start is called before the first frame update

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //if "move" in playercontroller is True

        if (PlayerController.instance.moving|RageSystem.instance.RageState) {
            if (other.gameObject.tag == "Enemy")
            {
                killscore += 1;
                UI_killscore.text = killscore.ToString()+"人斩";
                Destroy(other.gameObject.GetComponent<MeshRenderer>());
                other.gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
                Destroy(other.gameObject, 3);// destroy the Enemy and play destroy deconstruction animation;
                RageSystem.instance.AddRageValue();
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
