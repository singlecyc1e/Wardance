using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Animator CrossfadeTransition;
    public Animator DoorTransition;
    public float TransitionTime = 1.0f;
    public TimeController TimeManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadMenu();
        }
    }

    public void LoadMenu()
    {
        StartCoroutine(CoroutineLoadMenu());

        GameObject.Find("SwordCollider").GetComponent<WeaponDMG>().Alive = true;
    }

    IEnumerator CoroutineLoadMenu()
    {
        //Time.timeScale = 0;

        CrossfadeTransition.SetTrigger("Start");
        DoorTransition.SetTrigger("Start");

        yield return new WaitForSeconds(TransitionTime);

        SceneManager.LoadScene(0);
    }
}
