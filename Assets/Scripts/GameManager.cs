using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    Endgame endgame;


    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        endgame = FindObjectOfType<Endgame>();
    }
    // Start is called before the first frame update
    void Start()
    {
        

        quiz.gameObject.SetActive(true);
        endgame.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(quiz.isComplete)
        {
            quiz.gameObject.SetActive(false);
            endgame.gameObject.SetActive(true);
            endgame.ShowFinal();
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
