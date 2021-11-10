using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeCompleteQuestion = 30f;
    [SerializeField] float timeShowCorrect = 30f;

    public bool loadNext;
    public float fillTime;

    public bool isAnsweringQuestion;
    float timeValue;

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timeValue = 0;
    }

    void UpdateTimer()
    {
        timeValue -= Time.deltaTime;

        //if answering a question
        if(isAnsweringQuestion)
        {
            // still have time
            if(timeValue >0)
            {
                fillTime = timeValue / timeCompleteQuestion;
            }
            else
            {
                isAnsweringQuestion = false;
                timeValue = timeShowCorrect;
            }
        }
        else
        {
            if (timeValue > 0)
            {
                fillTime = timeValue / timeShowCorrect;
            }
            else
            {
                isAnsweringQuestion = true;
                timeValue = timeCompleteQuestion;
                loadNext = true;
            }
        }
    }

}
