using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score;
    public int antes;
    public Text scoreText;
    public void Pontuar(int soma)
    {
        antes = int.Parse(scoreText.text);
        score = antes + soma;
        scoreText.text = score.ToString();
    }

    public int CountFPS = 30;
    public float Duration = 1f;
    private IEnumerator CountText(int newValue)
    {
        yield return new WaitForSeconds(1f / CountFPS);

        //int previousValue = int.Parse(scoreText.text);
        //int stepAmount;

        //stepAmount = Mathf.CeilToInt((newValue - previousValue) / (CountFPS * Duration)); // newValue = 20, previousValue = 0. CountFPS = 30, and Duration = 1; (20 - 0) / (30*1) // 0.66667 (floortoint)-> 0

        //while (previousValue < newValue)
        //{
        //    previousValue += stepAmount;
        //    if (previousValue > newValue)
        //    {
        //        previousValue = newValue;
        //    }

        //    scoreText.text = previousValue.ToString();

        //    yield return Wait;            
        //}
        //score = 0;
    }

    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    void Update()
    {
        //if (score > 0)
        //{
        //    StartCoroutine(CountText(score));
        //}
    }
}
