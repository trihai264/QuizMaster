using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Question")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<Question> questions = new List<Question>();
    Question currentquestion;

    [Header("Answer")]
    [SerializeField] GameObject[] answerButton;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header("buttons")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoretext;
    Score scoreKeep;

    [Header("Bar")]
    [SerializeField] Slider bar;

    public bool isComplete;


    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeep = FindObjectOfType<Score>();
        bar.maxValue = questions.Count;
        bar.value = 0;
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillTime;

        if(timer.loadNext)
        {
            if (bar.value == bar.maxValue)
            {
                isComplete = true;
                return;
            }
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNext = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            displayAnswer(-1);
            SetButtonState(false);
        }
    }

    //Button Interactions
    public void onAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        displayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoretext.text = "Score: " + scoreKeep.CalculateScore() + "%";

       
    }

    void displayAnswer(int index)
    {
        Image buttonImage;
        if (index == currentquestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            buttonImage = answerButton[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeep.IncrementCorrectAns();
        }
        else
        {
            correctAnswerIndex = currentquestion.GetCorrectAnswerIndex();
            string correctAnswer = currentquestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Too bad, the correct answer was;\n" + correctAnswer;
            buttonImage = answerButton[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }

    }

    //go to the next question
    void GetNextQuestion()
    {
        if(questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultSprites();
            GetRandomQ();
            DisplayQuestion();
            bar.value++;
            scoreKeep.IncrementQuesSeen();
        }
        
    }

    void GetRandomQ()
    {
        int index = Random.Range(0, questions.Count);
        currentquestion = questions[index];

        if(questions.Contains(currentquestion))
        {
            questions.Remove(currentquestion);
        }
        
    }

    //get questions and answers
    void DisplayQuestion ()
    {
        questionText.text = currentquestion.GetQuestion();

        for (int i = 0; i < answerButton.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButton[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentquestion.GetAnswer(i);
        }

    }

    //set buttons
    void SetButtonState(bool state)
    {
        for(int i = 0; i < answerButton.Length; i++)
        {
            Button button = answerButton[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    //set buttons back to default
    void SetDefaultSprites()
    {
        for (int i = 0; i < answerButton.Length; i++)
        {
            Image buttonImage = answerButton[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
}
