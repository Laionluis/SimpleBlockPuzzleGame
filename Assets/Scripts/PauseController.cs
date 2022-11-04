using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pausePanel;

    public GameObject botaoPause;

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        botaoPause.SetActive(false);
    }
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        botaoPause.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
