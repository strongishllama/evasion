/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Author:         Taliesin Millhouse & Emma Cameron                                                                                              //
//  Date Created:   27th October 2016                                                                                                              //
//  Brief:          LevelManager Class dictates what level will be loaded next on scene completion.                                                //
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    bool m_bIsTransitioning = false;

    public FadeInOut m_FadeInOut;

	public SoundManager m_SoundManager;

	public OtherSounds m_OtherSounds;

    /// <summary>
    /// void OnCollision : Is called when the player collides with the end scene trigger.
    /// </summary>
    /// <param name="Player"></param>
   void OnTriggerStay(Collider Player)
    {
        if (m_bIsTransitioning)
        {
            return;
        }

        // Switch Statement to check what scene needs to be loaded.
        switch (SceneManager.GetActiveScene().name)
        {
            // Level One.
            case "Level_01":
                StartCoroutine(LoadNextScene("Level_02"));
                break;
            
            // Level Two.
            case "Level_02":
                StartCoroutine(LoadNextScene("Level_03"));
                break;
            
            // Level Three.
            case "Level_03":
                StartCoroutine(LoadNextScene("Level_04"));
                break;
            
            // Level Four.
            case "Level_04":
                StartCoroutine(LoadNextScene("Level_05"));
                break;
            
            // Level Five.
            case "Level_05":
                StartCoroutine(LoadNextScene("Level_06"));
                break;

            // Level Five.
            case "Level_06":
                StartCoroutine(LoadNextScene("Level_07"));
                break;

            // Level Five.
            case "Level_07":
                StartCoroutine(LoadNextScene("Level_08"));
                break;

            // Level Five.
            case "Level_08":
                StartCoroutine(LoadNextScene("Level_09"));
                break;

            // Level Five.
            case "Level_09":
                StartCoroutine(LoadNextScene("EndCutScene"));
                break;

            // Default: Send Debug Log to inform that the Default Option was Triggered.
            default:
                Debug.Log("LevelManager EnemyDetect Switch Statement default Option Triggered (Something is wrong!).");
                break;
        }
    }

    IEnumerator LoadNextScene(string a_strLevelName)
    {
        m_bIsTransitioning = true;
		m_OtherSounds.LevelCompleteSound();
		yield return new WaitForSeconds(0.0f);
        m_FadeInOut.EndScene(a_strLevelName);
    }
}
