using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}