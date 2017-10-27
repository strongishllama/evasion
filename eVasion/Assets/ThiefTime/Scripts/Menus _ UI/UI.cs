using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Button m_Button;
    public Color m_NewColor;

    public GameManager m_GameManager;

    public void DashIsReady()
    {
        if (m_GameManager.m_Player.m_bDashNextMove)
        {
            Debug.Log("color");
            m_NewColor.r = 255.0f;
            m_NewColor.g = 0.0f;
            m_NewColor.b = 0.0f;

            ColorBlock m_ColorBlock = m_Button.colors;
            m_ColorBlock.normalColor = m_NewColor;
            m_Button.colors = m_ColorBlock;
        }
    }
}
