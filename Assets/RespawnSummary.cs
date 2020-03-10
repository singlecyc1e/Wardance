using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnSummary : MonoBehaviour
{
    int LifeRemain;

    public Text LifeCount;
    public Text LifeText;
    public GameObject Life2;
    public GameObject Life3;

    // Start is called before the first frame update
    void Start()
    {
        EnterScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnterScene()
    {
        LifeRemain = PlayerPrefs.GetInt(LevelController.playerLifeKey);
        Debug.Log(LifeRemain);

        LifeCount.text = LifeRemain.ToString() + "             ";

        if (LifeRemain == 2)
        {
            Life3.GetComponent<Animator>().SetTrigger("Play");
        }

        if (LifeRemain == 1)
        {
            LifeText.text = "    Life Remainning";
            Life3.SetActive(false);
            Life2.GetComponent<Animator>().SetTrigger("Play");
        }
    }
}
