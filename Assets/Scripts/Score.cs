using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score;
    public int antes;
    public Text scoreText;
    public bool animar;
    public Vector3 initialOffset = new Vector3(0.79f, -1.86f, 1);
    public Vector3 finalOffset = new Vector3(2.46f, 5f, 1);
    public GameObject texto;

    public float fadeDuration;
    private float fadeStartTime;

    private GameObject text;

    public void Pontuar(int soma, Vector3? posa)
    {
        antes = int.Parse(scoreText.text);
        score = antes + soma;
        scoreText.text = score.ToString();
        if(posa != null)
        {
            initialOffset = posa.Value;
            finalOffset = new Vector3(2.46f, 5f, 1);
            animar = true;

            text = Instantiate(texto);
            text.transform.GetComponent<TextMeshPro>().SetText(soma.ToString());
            fadeStartTime = Time.time;
        }        
    }

    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    void Update()
    {
        if (animar)
        {
            float progress = (Time.time - fadeStartTime) / .4f;
            if (progress <= 1)
            {
                text.transform.localPosition = Vector3.Lerp(initialOffset, finalOffset, progress);
            }
            else
            {
                Destroy(text);
                animar = false;
            }
        }
    }
}
