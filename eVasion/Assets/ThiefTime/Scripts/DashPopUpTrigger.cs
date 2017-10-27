using UnityEngine;
using System.Collections;

public class DashPopUpTrigger : MonoBehaviour
{
    public GameManager m_GameManager;

    public bool m_bPopUpPlayed = false;

    public void OnTriggerStay(Collider Player)
    {
        StartCoroutine("DashPrompt");
        m_bPopUpPlayed = true;
    }

    IEnumerator DashPrompt()
    { 
        if (!m_bPopUpPlayed)
        {
            m_GameManager.m_DashPromt.SetActive(true);
            yield return new WaitForSeconds(7.0f);
            m_GameManager.m_DashPromt.SetActive(false);
            StopCoroutine("DashPrompt");
        }
    }
}