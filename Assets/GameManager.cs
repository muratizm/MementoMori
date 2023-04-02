using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject videoObject;
    [SerializeField] VideoPlayer videoPlayer;
    public GameObject pauseMenu;
    public GameObject entryMenu;
    public GameObject dieMenu;
    private void Start()
    {
        Time.timeScale = 0f;
        Invoke("Flashback", 1f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

    }

    public void PauseGame()
    {
        // Pause the game and show the pause menu
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }


    public void StartGame()
    {
        entryMenu.SetActive(false);
        Time.timeScale = 0.86f;
    }


    public void ResumeGame()
    {
        // Unpause the game and hide the pause menu
        Time.timeScale = 0.86f;
        pauseMenu.SetActive(false);
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }

    public void GoCheckpoint()
    {
        PlayerCombat pc = FindObjectOfType<PlayerCombat>();
        pc.Attack();
        pc.enabled = true;
        pc.Invoke("Start", 0.1f);
        pc.gameObject.transform.position = pc.gameObject.GetComponent<PlayerMovement>().currentCheckpoint;
        pc.isDead = false;
    }

    public void DieGame()
    {
        Time.timeScale = 0;
        dieMenu.SetActive(true);
    }

    public void RevealGame()
    {
        Time.timeScale = 0.86f;
        dieMenu.SetActive(false);
        GoCheckpoint();
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