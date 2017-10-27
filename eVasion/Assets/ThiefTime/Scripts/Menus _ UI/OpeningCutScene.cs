using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpeningCutScene : MonoBehaviour
{
    public Image[] m_Images;

    FadeInOut m_FadeInOut;

    private string m_strScene = "Level_01";

	int iCount = 0;

	void Awake()
    {
        m_FadeInOut = GameObject.FindObjectOfType<FadeInOut>();
    }

    void Start ()
    {   
        for (int iCount = 1; iCount < 14; ++iCount)
        {
            m_Images[iCount].enabled = false;
        }
	}

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Space))
		{
			++iCount;
			Debug.Log(iCount);
			if (iCount >= 14)
			{
				m_FadeInOut.EndScene(m_strScene);
			}

			m_Images[iCount].enabled = true;
			m_Images[iCount - 1].enabled = false;
		}
	}
}
