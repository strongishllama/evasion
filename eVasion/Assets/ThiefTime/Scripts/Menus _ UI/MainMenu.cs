//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//  Author:         Taliesin Millhouse & Emma Cameron
//  Date Created:   21st October 2016
//  Brief:          Main Menu Class Controls the Main Menu.
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// public SoundManager : Initializing the SoundManager.
    /// </summary>
    public SoundManager m_SoundManager;

    public Texture[] m_Textures;

    FadeInOut m_FadeInOut;

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void Awake()
    {
        m_FadeInOut = GameObject.FindObjectOfType<FadeInOut>();
    }

    /// <summary>
    /// public void : OnButtonClick takes a string for a parameter.
    /// </summary>
    /// <param name="a_strButton"></param>
    public void OnButtonClick(string a_strButton)
    {
        // Switch Statement to check what button is pressed.
        switch (a_strButton)
        {
            // Start New Game: Play Opening Cut Scene.
            case "NEW":
                m_FadeInOut.EndScene("OpeningCutScene");
                break;

            // Load Menu: Load Level One.
            case "LOAD_LEVEL_ONE":
                m_FadeInOut.EndScene("Level_01");
                break;

            // Load Menu: Load Level Two.
            case "LOAD_LEVEL_TWO":
                m_FadeInOut.EndScene("Level_02");
                break;

            // Load Menu: Load Level Three.
            case "LOAD_LEVEL_THREE":
                m_FadeInOut.EndScene("Level_03");
                break;

            // Load Menu: Load Level Four.
            case "LOAD_LEVEL_FOUR":
                m_FadeInOut.EndScene("Level_04");
                break;

            // Load Menu: Load Level Five.
            case "LOAD_LEVEL_FIVE":
                m_FadeInOut.EndScene("Level_05");
                break;

            // Load Menu: Load Level Six.
            case "LOAD_LEVEL_SIX":
                m_FadeInOut.EndScene("Level_06");
                break;

            // Load Menu: Load Level Seven.
            case "LOAD_LEVEL_SEVEN":
                m_FadeInOut.EndScene("Level_07");
                break;

            // Load Menu: Load Level Eight.
            case "LOAD_LEVEL_EIGHT":
                m_FadeInOut.EndScene("Level_08");
                break;

            // Options Menu: Sound On.
            case "SOUND_ON":
                m_SoundManager.m_AudioSourceOne.volume = 1.0f;
                m_SoundManager.m_AudioSourceTwo.volume = 1.0f;
                m_SoundManager.m_AudioSourceThree.volume = 1.0f;
                break;

            // Options Menu: Sound Off.
            case "SOUND_OFF":
                m_SoundManager.m_AudioSourceOne.volume = 0.0f;
                m_SoundManager.m_AudioSourceTwo.volume = 0.0f;
                m_SoundManager.m_AudioSourceThree.volume = 0.0f;
                break;

            // Quit Menu: Quit Game.
            case "QUIT":
                Application.Quit();
                break;

            // Default: Send Debug Log to inform that the Default Option was Triggered.
            default:
                Debug.Log("MainMenu OnButtonClick Switch Statement Default Option Triggered (Something is wrong!).");
                break;
        }
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
}
