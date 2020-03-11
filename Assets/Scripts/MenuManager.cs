using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Animator CrossfadeTransition;
    public Animator DoorTransition;

    public float TransitionTime = 1.0f;
    public float DeathTimeScale = 0.1f;

    // Update is called once per frame

    private void Start()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    public void LoadMenu(string SceneName)
    {
        StartCoroutine(CoroutineLoadMenu(SceneName));
    }

    IEnumerator CoroutineLoadMenu(string SceneName)
    {

        CrossfadeTransition.SetTrigger("Start");
        DoorTransition.SetTrigger("Start");

        Time.timeScale = DeathTimeScale;

        yield return new WaitForSeconds(TransitionTime * DeathTimeScale);

        //Load Menu
        SceneManager.LoadScene(SceneName);
    }


    public void LoadNextLevel()
    {
        StartCoroutine(LoadNext());
    }

    IEnumerator LoadNext()
    {

        CrossfadeTransition.SetTrigger("Start");
        DoorTransition.SetTrigger("Start");

        Time.timeScale = DeathTimeScale;

        yield return new WaitForSeconds(TransitionTime * DeathTimeScale);

        //Load Menu
        SceneManager.LoadScene("FinalStart");
    }
}
