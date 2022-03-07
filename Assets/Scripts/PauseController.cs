using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pausePanel;

    public void PauseGame()
    {
        pausePanel.SetActive(true);
    }
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
