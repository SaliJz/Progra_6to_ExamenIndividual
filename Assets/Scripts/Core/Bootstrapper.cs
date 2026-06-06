using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return null;
        SceneManager.LoadScene("MainMenu");
    }
}