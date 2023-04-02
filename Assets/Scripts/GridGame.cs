using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridGame : MonoBehaviour
{
    [SerializeField] private GameObject RewardPage;
    [SerializeField] private TMP_Text instructionsText;
    [SerializeField] private Button[,] gridButtons;
    private bool isPlaying;
    private int numPressed;
    private int greenPressed;
    private List<int> pressIndices;
    private List<int> greenIndices;

    void Start()
    {
        isPlaying = true;
        numPressed = 0;
        greenPressed = 0;
        pressIndices = new List<int>();
        greenIndices = new List<int>();

        GameObject[] objects = GameObject.FindGameObjectsWithTag("ButtonObject");
        // Get references to all the grid buttons
        gridButtons = new Button[5, 5];
        int count = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                gridButtons[i, j] = objects[count].GetComponent<Button>();
                count++;
            }
        }



        // Choose 5 random green buttons and light them up
        for (int i = 0; i < 5; i++)
        {
            int randomX = Random.Range(0, 5);
            int randomY = Random.Range(0, 5);

            // Check if this position is already selected as green
            bool isAlreadyGreen = false;
            for (int j = 0; j < greenIndices.Count; j++)
            {
                int index = greenIndices[j];
                int x = index / 5;
                int y = index % 5;
                if (randomX == x && randomY == y)
                {
                    isAlreadyGreen = true;
                    break;
                }
            }

            // If this position is already selected as green, choose another position
            if (isAlreadyGreen)
            {
                i--; // Reduce i to try selecting another green button
                continue; // Skip adding this position to greenIndices and re-try
            }

            // Otherwise, select this position as green and add it to the list
            gridButtons[randomX, randomY].image.color = Color.green;
            greenIndices.Add(randomX * 5 + randomY);
        }

        Invoke("setGrey", 0.5f);


        instructionsText.text = "You have 5 choices.";
    }


    public void setGrey()
    {
        // Set all buttons to gray
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                gridButtons[i, j].image.color = Color.gray;
            }
        }
    }

    public void TryAgain()
    {
        Invoke("Start", 0.1f);
    }

    public void ButtonClick(string name)
    {
        int index = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().transform.GetSiblingIndex();
        // Do something with the button index
        Debug.Log("Button " + index + " was clicked!");
        numPressed++;
        Debug.Log("num :" + numPressed);
        if (greenIndices.Contains(index))
        {
            greenIndices.Remove(index);
            greenPressed++;
            Debug.Log("green: " + greenPressed);
        }

        Check();
    }

    public void Check()
    {
        if (numPressed == 5)
        {
            if (greenPressed == 5)
            {
                instructionsText.text = "Won!";
                Invoke("Win", 0.5f);
            }
            else
            {
                instructionsText.text = "Try again.";
            }
        }
    }
    public void Win()
    {
        gameObject.SetActive(false);
        RewardPage.SetActive(true);
    }
}

