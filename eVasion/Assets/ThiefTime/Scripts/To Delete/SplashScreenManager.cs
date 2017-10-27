using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{
    bool m_bIsTransitioning = false;
    
	// Use this for initialization
	IEnumerator LoadNextStage(string a_strLevelName, float a_fFadeTime)
    {
        m_bIsTransitioning = true;
        yield return new WaitForSeconds(a_fFadeTime);
        SceneManager.LoadScene(a_strLevelName);
    }

    void Update()
    {
        StartCoroutine(LoadNextStage("MainMenu", 3));
        if (m_bIsTransitioning)
        {
            return;
        }
        //float m_fFadeTime = GameObject.Find("GameManager").GetComponent<TransitionFadeInOut>().BeginFade(1);
    }
}