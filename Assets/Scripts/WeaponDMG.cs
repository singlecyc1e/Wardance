using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WeaponDMG : MonoBehaviour
{
    public Text UI_killscore; 
    public float killscore = 0;
    public static WeaponDMG instance;
    public bool Alive = true;
    public TimeController timeManager;
    //public bool BulletTime = false;
    public TextMeshProUGUI chineseword;
    public float displayduration = .2f;
    public float timecheck = 1.5f;
    private float killtime = 0f;
    private int counter = 1;
    //public TimeController Timemanager;
    // Start is called before the first frame update

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }


    }


    private void Start()
    {
        Alive = true;
        chineseword.enabled = false;
    }

    public void SetupDeathMenu() {
        AudioSystem.instance.DeathAudio.Invoke();
        Alive = false;

        PlayerPrefs.SetInt("CurrentScore", (int)killscore);
        PlayerPrefs.SetInt("CurrentDistance", (int)Time.timeSinceLevelLoad);

        GameObject.Find("MenuManager").GetComponent<MenuManager>().LoadMenu("SiciliaMenu");
    }

    public void SetupResponseMenu()
    {
        AudioSystem.instance.DeathAudio.Invoke();
        Alive = false;

        PlayerPrefs.SetInt("CurrentScore", (int)killscore);
        PlayerPrefs.SetInt("CurrentDistance", (int)Time.timeSinceLevelLoad);

        GameObject.Find("MenuManager").GetComponent<MenuManager>().LoadMenu("RespawnMenu");
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Block")
        {
            if (RageSystem.instance.inRageMode)
            {
                Destroy(other.gameObject.GetComponent<MeshRenderer>());
                other.gameObject.transform.GetComponentInChildren<ParticleSystem>().Play();
    
                Destroy(other.gameObject, 3);// destroy the Enemy and play destroy deconstruction animation;
                //other.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
                Destroy(other.gameObject, 3);// destroy the Enemy and play destroy deconstruction animation;
            }
            else
            {
                //SetupDeathMenu();
                LevelController.DecrementLife();
            }
        }

        if (other.gameObject.tag == "Regular")
        {
            //if "move" in playercontroller is True
            if (PlayerController.instance.moving | RageSystem.instance.inRageMode | PlayerController.instance.slashing)
            {
                WordDisplay();
                killtime = Time.time;
                killscore += 1;
                UI_killscore.text = killscore.ToString();
                Destroy(other.gameObject.GetComponent<MeshRenderer>());
                other.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
                Destroy(other.gameObject, 3);// destroy the Enemy and play destroy deconstruction animation;
                RageSystem.instance.AddRageValue();
                //if (BulletTime)
                //{
                //    timeManager.BulletTime();
                //}
            }
            else
            {
                // SetupDeathMenu();
                LevelController.DecrementLife();
            }
        }

        else if (other.gameObject.tag == "Spear" && !RageSystem.instance.inRageMode && !PlayerController.instance.slashing)
        {
            //Debug.Log("Spear");
                // SetupDeathMenu();
                LevelController.DecrementLife();
        }

        if (other.gameObject.tag == "HeavyArmor")
        {

            //if "move" in playercontroller is True
            if (RageSystem.instance.inRageMode)
            {
                WordDisplay();
                killtime = Time.time;
                killscore += 1;
                UI_killscore.text = "x " + killscore.ToString();
                Destroy(other.gameObject.GetComponent<MeshRenderer>());
                other.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
                Destroy(other.gameObject, 3);// destroy the Enemy and play destroy deconstruction animation;
                RageSystem.instance.AddRageValue();
                //if (BulletTime)
                //{
                //    timeManager.BulletTime();
                //}
            }
            else
            {
                // SetupDeathMenu();
                LevelController.DecrementLife();
            }
        }
        //if "move" in playercontroller is False
        //gameover, pause the game
    }

    private void WordDisplay()
    {
        AudioSystem.instance.EnemydieAudio.Invoke();
        if ((Time.time - killtime) < timecheck)
        {
            counter++;
            StartCoroutine(Display());
        }
        else
        {
            counter = 1;
        }
    }

    IEnumerator Display()
    {
        chineseword.enabled = true;
        if (counter == 0)
        {
            chineseword.text = "";
        }
        else if (counter == 1)
        {
            chineseword.text = "";
        }
        else if (counter == 2)
        {
            chineseword.text = "贰连斩";
        }
        else if (counter == 3)
        {
            chineseword.text = "叁连斩";
        }
        else if (counter == 4)
        {
            chineseword.text = "肆连斩";
        }
        else if (counter == 5)
        {
            chineseword.text = "伍连斩";
        }
        else if (counter == 6)
        {
            chineseword.text = "陆连斩";
        }
        else if (counter == 7)
        {
            chineseword.text = "柒连斩";
        }
        else if (counter == 8)
        {
            chineseword.text = "超凡入圣";
        }
        else if (counter == 9)
        {
            chineseword.text = "神挡杀神";
        }
        else if (counter == 10)
        {
            chineseword.text = "佛挡杀佛";
        }
        else if (counter == 11)
        {
            chineseword.text = "吾即天命";
        }
        else if (counter == 12)
        {
            chineseword.text = "拾贰连斩";
        }
        else if (counter == 13)
        {
            chineseword.text = "拾叁连斩";
        }
        else if (counter == 14)
        {
            chineseword.text = "拾肆连斩";
        }
        else if (counter == 15)
        {
            chineseword.text = "拾伍连斩";
        }
        chineseword.rectTransform.Rotate(0.0f, 0.0f, UnityEngine.Random.Range(-10.0f, 10.0f));
        yield return new WaitForSeconds(displayduration);
        chineseword.rectTransform.rotation = Quaternion.identity;
        chineseword.text = "";
        chineseword.enabled = false;

    }
}
