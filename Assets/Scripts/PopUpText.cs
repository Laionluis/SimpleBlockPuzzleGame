using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class PopUpText : MonoBehaviour
{
    [HideInInspector]
    public string displayText;

    public float fadeDuration;
    private float fadeStartTime;

    // Start is called before the first frame update
    void Start()
    {
        fadeStartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
