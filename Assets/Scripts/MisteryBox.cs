using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisteryBox : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas;

    private bool inTriggerArea;
    private bool menuOpen;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inTriggerArea = true;
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.currentCheckpoint = new Vector2(transform.position.x + 0.5f, transform.position.y + 2);
                Debug.Log(playerMovement.currentCheckpoint);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inTriggerArea = false;
            CloseMenu();
        }
    }

    private void Update()
    {
        if (inTriggerArea && Input.GetKeyDown(KeyCode.E))
        {
            if (!menuOpen)
            {
                OpenMenu();
            }
            else
            {
                CloseMenu();
            }
        }

        if (inTriggerArea && menuOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMenu();
        }
    }

    private void OpenMenu()
    {
        menuOpen = true;
        menuCanvas.SetActive(true);
    }

    private void CloseMenu()
    {
        menuOpen = false;
        menuCanvas.SetActive(false);
    }


    
}
