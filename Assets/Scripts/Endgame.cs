using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Endgame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalText;
    Score score;
    // Start is called before the first frame update
    void Awake()
    {
        score = FindObjectOfType<Score>();
    }

    public void ShowFinal()
    {
        finalText.text = "Congratulations!\n You got a score of " + score.CalculateScore() + "%";
    }

}
