using UnityEngine;
using System.Collections;

public class MainMenuSoundOnOff : MonoBehaviour
{
    public Texture[] m_Textures;

    public SoundManager m_SoundManager;

    void OnGUI()
    {
        if (m_SoundManager.m_AudioSourceOne.volume == 0.0f)
        {
            GUI.DrawTexture(new Rect(820, 900, 35, 30), m_Textures[0]);
            GUI.DrawTexture(new Rect(820, 900, 35, 30), m_Textures[1]);
        }
        else if (m_SoundManager.m_AudioSourceOne.volume == 1.0f)
        {
            GUI.DrawTexture(new Rect(820, 900, 35, 30), m_Textures[0]);
        }
    }
}
