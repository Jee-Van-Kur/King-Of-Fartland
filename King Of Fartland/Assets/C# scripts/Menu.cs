using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public GameObject PauseMenuUI;
    public shake shaker;

    // Update is called once per frame
    private void Start()
    {
        Time.timeScale = 0f;
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Load();
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        shaker.camEnd();
        Invoke("Load", 0.5f);
    }

    public void How()
    {

    }

    private void Load()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
