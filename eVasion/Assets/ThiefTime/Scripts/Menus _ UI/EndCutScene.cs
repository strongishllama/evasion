using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndCutScene : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Credits");
    }
}