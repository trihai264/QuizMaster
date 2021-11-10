using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    int correct = 0;
    int quesSeen = 0;

    public int GetCorrectAns()
    {
        return correct;
    }

    public void IncrementCorrectAns()
    {
        correct++;
    }

    public int getQuestSeen()
    {
        return quesSeen;
    }

    public void IncrementQuesSeen()
    {
        quesSeen++;
    }

    public int CalculateScore()
    {
        return Mathf.RoundToInt( correct / (float)quesSeen * 100);
    }
}
