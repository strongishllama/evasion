using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    FadeInOut m_FadeInOut;
    public string m_strScene;

    void Awake()
    {
        m_FadeInOut = GameObject.FindObjectOfType<FadeInOut>();
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        m_FadeInOut.EndScene(m_strScene);
    }
}