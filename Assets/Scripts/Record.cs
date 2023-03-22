using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Record : MonoBehaviour
{
    public Text recordText;
    public Text scoreText;

    public void SalvarRecord()
    {
        int recordatual = PlayerPrefs.GetInt("Record");
        int score = int.Parse(scoreText.text);
        if(score > recordatual)
        {
            PlayerPrefs.SetInt("Record", score);
            recordText.text = scoreText.text;
        }           
    }

    // Start is called before the first frame update
    void Start()
    {
        recordText = GetComponent<Text>();
        int recordatual = PlayerPrefs.GetInt("Record");
        recordText.text = recordatual.ToString();
    }
}
