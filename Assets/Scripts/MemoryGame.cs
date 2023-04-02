using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;


public class MemoryGame : MonoBehaviour
{
    [SerializeField] GameObject videoObject;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] public TMP_Text instructionsText;
    private List<Sprite> normalPhotos;
    private List<Sprite> selectedPhotos;
    public Button[] buttons;

    int numPressed;
    int truePressed;

    void Start()
    {
        numPressed = 0;
        truePressed = 0;
        foreach (var button in buttons)
        {
            button.interactable = true;
        }
        instructionsText.text = "Memorize 3 items that you gain from memories";
        normalPhotos = Resources.LoadAll<Sprite>("Sprites/icons/others/black_background").ToList();
        selectedPhotos = Resources.LoadAll<Sprite>("Sprites/icons/main/black_background").ToList();

        // Merge the photos
        List<Sprite> allPhotos = new List<Sprite>();
        allPhotos.AddRange(selectedPhotos);
        allPhotos.AddRange(normalPhotos);

        // Shuffle the photos
        allPhotos = allPhotos.OrderBy(x => Guid.NewGuid()).ToList();

        // Assign the photos to the buttons
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Image>().sprite = allPhotos[i];
        }
    }

    public void ButtonClick(string name)
    {
        Sprite spr = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().GetComponent<Image>().sprite;
        // Do something with the button index
        numPressed++;
        Debug.Log("num :" + numPressed);
        if (selectedPhotos.Contains(spr))
        {
            truePressed++;
            Debug.Log(truePressed);
        }

        Check();
    }


    public void TryAgain()
    {
        Invoke("Start", 0.1f);
    }

    public void Check()
    {
        if (numPressed == 3)
        {
            foreach (var button in buttons)
            {
                button.interactable = false;
            }

            if (truePressed == 3)
            {
                instructionsText.text = "You won!";
                Invoke("Win", 0.5f);
            }
            else
            {
                instructionsText.text = "You lost! Try again.";
            }
        }
    }
    public void Win()
    {
        gameObject.SetActive(false);
        Flashback();
    }
    private void OnVideoFinished(VideoPlayer vp)
    {
        videoObject.SetActive(false);
        vp.Stop();
    }
    public void Flashback()
    {
        videoPlayer.Play();
        videoObject.SetActive(true);
        videoPlayer.time = 0f;

        videoPlayer.loopPointReached += OnVideoFinished;
    }
}
