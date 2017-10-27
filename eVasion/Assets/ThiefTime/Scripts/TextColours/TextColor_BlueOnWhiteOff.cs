using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextColor_BlueOnWhiteOff : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text m_Text;

    public Color m_Color;

    public void OnPointerEnter(PointerEventData a_EvenData)
    {
        m_Color.r = 0.0f;
        m_Color.g = 170.0f;
        m_Color.b = 255.0f;
        m_Color.a = 255.0f;
        m_Text.color = m_Color;
    }

    public void OnPointerExit(PointerEventData a_EventData)
    {
        m_Color.r = 255.0f;
        m_Color.g = 255.0f;
        m_Color.b = 255.0f;
        m_Color.a = 255.0f;
        m_Text.color = m_Color;
    }
}
