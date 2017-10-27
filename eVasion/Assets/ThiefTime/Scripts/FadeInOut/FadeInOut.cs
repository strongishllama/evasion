using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class FadeInOut : MonoBehaviour
{

    public Image m_FadeImage;
    public float m_fFadeSpeed = 1.5f;
    public bool m_bStartScene = true;

    void Awake()
    {
        m_FadeImage.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bStartScene)
        {
            StartScene();
        }
    }

    void FadeIn()
    {
        m_FadeImage.color = Color.Lerp(m_FadeImage.color, Color.clear, m_fFadeSpeed * Time.deltaTime);
    }

    void FadeOut()
    {
        m_FadeImage.color = Color.Lerp(m_FadeImage.color, Color.black, m_fFadeSpeed * Time.deltaTime);
    }

    void StartScene()
    {
        FadeIn();

        if (m_FadeImage.color.a <= 0.05f)
        {
            m_FadeImage.color = Color.clear;
            m_FadeImage.enabled = false;

            m_bStartScene = false;
        }
    }

    public IEnumerator EndSceneRoutine(string a_strScene)
    {
        m_FadeImage.enabled = true;

        do
        {
            FadeOut();

            if (m_FadeImage.color.a >= 0.95f)
            {
                SceneManager.LoadScene(a_strScene);
                yield break;
            }
            else
            {
                yield return null;
            }
        }
        while (true);
    }

    public void EndScene(string a_strScene)
    {
        m_bStartScene = false;
        StartCoroutine("EndSceneRoutine", a_strScene);
    }
}