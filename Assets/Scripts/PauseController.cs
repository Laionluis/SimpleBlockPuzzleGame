using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pausePanel;

    public GameObject botaoPause;

    public GameObject gameOverPainel;

    public GameObject botaoPlay;

    public bool gameOver;

    public void PauseGame()
    {
        if (gameOver)
        {
            pausePanel.SetActive(true);
            botaoPause.SetActive(false);
            botaoPlay.SetActive(false);
            gameOverPainel.SetActive(true);
        }
        else
        {
            pausePanel.SetActive(true);
            botaoPlay.SetActive(true);
            botaoPause.SetActive(false);
            gameOverPainel.SetActive(false);
        }
      
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
