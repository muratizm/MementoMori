using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour { 


    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided GameObject has the playerTag
        if (collision.CompareTag("Player"))
        {
            Invoke("Flashback", 1f); // flashback
            // Get the PlayerCombat component from the collided GameObject and deal damage
            PlayerCombat playerCombat = collision.GetComponent<PlayerCombat>();
            if (playerCombat != null)
            {
                //playerCombat.TakeDamage(playerCombat.currentHealth);

                playerCombat.isDead = true;


                playerCombat.animator.SetTrigger("Hurt");
                playerCombat.animator.SetBool("isDead", true);

                Invoke("NewScene", 1f);
                
                
            }

            
        }
    }

    private void NewScene()
    {

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }


}
