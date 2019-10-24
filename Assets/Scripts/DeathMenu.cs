using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void Reset()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
