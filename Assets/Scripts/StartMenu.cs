using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* 
 * =======================================
 *        WELCOME TO THE SANDBOX!
 * =======================================
 * I am using these scripts as starters for
 * common scripts that most games will need
 * (menus, movers, etc). These scripts will
 * each be labeled and commented to explain
 * their use. You're welcome!
 *                            
 *                            - Past Aidan
 */

/*
 * =======================================
 *              START MENU
 * =======================================
 * Use this script to create a start menu!
 * This script requires:
 *  - Nothing! It can sit on any GameObject
 *  
 *  Use buttons to call the menu functions.
 */


public class StartMenu : MonoBehaviour
{
    [SerializeField] Animator doorAnimator;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject creditsPanel;
    [SerializeField] FadeEffect fadeScreenEffect;
    [SerializeField] float startDelay = 1.5f ;

    // Starts the game
    // Adjust build index as needed
    public void PlayGame()
    {
        AkSoundEngine.PostEvent("levelOneState", this.gameObject);
        StartCoroutine(DoPlayGame());

        // Deactivate buttons and title screen
        foreach (Transform child in transform) {
            // Avoid deactivating itself
            if (child != transform && child != fadeScreenEffect.transform) {
                child.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator DoPlayGame()
    {
        // Wait for animation and sound to finish
        //AkSoundEngine.PostEvent("PlayButton", this.gameObject);
        AkSoundEngine.PostEvent("menuForward", this.gameObject);
        //doorAnimator.SetTrigger("StartGame");
        fadeScreenEffect.FadeSpeed = 1f / startDelay;
        fadeScreenEffect.TargetAlpha = 1f;
        yield return new WaitForSeconds(startDelay);

        // zoom camera
        /*for(int i = 0; i < 50; i++)
        {
            Camera.main.orthographicSize -= 0.1f;
            yield return new WaitForSeconds(0.01f);
        }*/

        // Load Game
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(1);
    }

    // Quits the game
    public void Quit()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }
    public void HideSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void HideCredits()
    {
        creditsPanel.SetActive(false);
    }

    public void Tutorial()
    {
        AkSoundEngine.PostEvent("transitionState", this.gameObject);
        StartCoroutine(DoTutorial());

        // Deactivate buttons and title screen
        foreach (Transform child in transform)
        {
            // Avoid deactivating itself
            if (child != transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator DoTutorial()
    {
        // Wait for animation and sound to finish
        //AkSoundEngine.PostEvent("PlayButton", this.gameObject);
        AkSoundEngine.PostEvent("menuForward", this.gameObject);
        //doorAnimator.SetTrigger("StartGame");
        yield return new WaitForSeconds(1.5f);

        // zoom camera
        /*for (int i = 0; i < 50; i++)
        {
            Camera.main.orthographicSize -= 0.1f;
            yield return new WaitForSeconds(0.01f);
        }*/

        // Load Game
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(3);
    }
}
