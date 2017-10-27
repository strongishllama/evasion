//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//  Author:         Taliesin Millhouse & Emma Cameron
//  Date Created:   1st November 2016
//  Brief:          Pause Menu Class Controls the Pause Menu.
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// pubic GameMangager : Initializing the GameManager.
    /// </summary>
    public GameManager m_GameManager;

    public SoundManager m_SoundManager;

    public bool m_bIsTransitioning = false;

    public FadeInOut m_FadeInOut;

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// public void : OnButtonClick takes a string for a parameter.
    /// </summary>
    /// <param name="a_strButton"></param>
    public void OnButtonClick(string a_strButton)
    {
        if (m_bIsTransitioning)
        {
            return;
        }

        // Switch Statement to check what button is pressed.
        switch (a_strButton)
        {
            // Pause Menu: Resume Game.
            case "RESUME":
                m_GameManager.m_PauseMenu.SetActive(!m_GameManager.m_PauseMenu.activeInHierarchy);
                break;

            // Pause Menu: Load Main Menu.
            case "LOAD_MAIN_MENU":
                StartCoroutine(LoadMainMenu("MainMenu"));
                break;

            // Pause Menu: Sound On.
            case "SOUND_ON":
                m_SoundManager.m_AudioSourceOne.volume = 1.0f;
                m_SoundManager.m_AudioSourceTwo.volume = 1.0f;
                m_SoundManager.m_AudioSourceThree.volume = 1.0f;
                break;

            // Pause Menu: Sound Off.
            case "SOUND_OFF":
                m_SoundManager.m_AudioSourceOne.volume = 0.0f;
                m_SoundManager.m_AudioSourceTwo.volume = 0.0f;
                m_SoundManager.m_AudioSourceThree.volume = 0.0f;
                break;

            // Pause Menu: Quit Game.
            case "QUIT":
                Application.Quit();
                break;

            // Default: Send Debug Log to inform that the Default Option was Triggered.
            default:
                Debug.Log("PauseMenu OnButtonClick Switch Statement Default Option Triggered (Something is wrong!).");
                break;
        }
    }

    IEnumerator LoadMainMenu(string a_strLevelName)
    {
        m_bIsTransitioning = true;
        yield return new WaitForSeconds(0.0f);
        m_GameManager.m_PauseMenu.SetActive(false);
        m_FadeInOut.EndScene(a_strLevelName);
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
}
