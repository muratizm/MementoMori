using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class NumberGame : MonoBehaviour
{
    [SerializeField] GameObject videoObject1;
    [SerializeField] VideoPlayer videoPlayer1;
    [SerializeField] GameObject videoObject2;
    [SerializeField] VideoPlayer videoPlayer2;
    public TMP_Text[] numberDisplay;
    public TMP_InputField[] answerFields;
    public TMP_Text resultDisplay;

    private int[] numbers;

    void Start()
    {
        GenerateNumbers();
    }

    private void Update()
    {
        bool isFull = true;
        for (int i = 0; i < 5; i++)
        {
            if(numberDisplay[i].ToString().Length == 0)
            {
                isFull = false;
                break;
            }
        }
        if (isFull)
        {

            CheckAnswers();
        }

    }

    public void CheckAnswers()
    {
        // Get the user's answers
        int[] answers = new int[5];
        for (int i = 0; i < 5; i++)
        {
            int.TryParse(answerFields[i].text, out answers[i]);
        }

        // Check the answers
        bool allCorrect = true;
        for (int i = 0; i < 5; i++)
        {
            if (answers[i] != numbers[i])
            {
                allCorrect = false;
                break;
            }
        }

        // Display the result
        if (allCorrect)
        {
            resultDisplay.text = "You win!";
            Invoke("Win", 0.5f);
        }
        else
        {
            resultDisplay.text = "Try again!";
        }
    }

    public void TryAgain()
    {
        for (int i = 0; i < 5; i++)
        {
            answerFields[i].text = "";
        }
        GenerateNumbers();
    }

    public void GenerateNumbers()
    {
        // Generate 5 random numbers between 1 and 10
        numbers = new int[5];
        for (int i = 0; i < 5; i++)
        {
            numbers[i] = Random.Range(1, 11);
        }

        // Display the numbers on the screen
        for (int i = 0; i < 5; i++)
        {
            numberDisplay[i].transform.parent.gameObject.SetActive(true);
            numberDisplay[i].text = numbers[i].ToString();
        }

        Invoke("DisableNumbers", 0.7f);
    }

    private void DisableNumbers()
    {
        // Not Display the numbers on the screen
        for (int i = 0; i < 5; i++)
        {
            numberDisplay[i].transform.parent.gameObject.SetActive(false);
            
        }
    }
        private void OnVideoFinished(VideoPlayer vp)
    {
            vp.Stop();
            videoObject1.SetActive(false);
            videoObject2.SetActive(false);
    }

    public void Win()
    {
        gameObject.SetActive(false);
        if(gameObject.name == "NumberGame1")
        {

            videoPlayer1.Play();
            videoObject1.SetActive(true);
            videoPlayer1.time = 0f;
            videoPlayer1.loopPointReached += OnVideoFinished;
        }
        else if(gameObject.name == "NumberGame2")
        {

            videoPlayer2.Play();
            videoObject2.SetActive(true);
            videoPlayer2.time = 0f;
            videoPlayer2.loopPointReached += OnVideoFinished;

        }
    }
}
