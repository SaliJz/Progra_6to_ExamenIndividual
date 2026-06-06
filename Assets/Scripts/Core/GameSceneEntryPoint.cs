using UnityEngine;

public class GameSceneEntryPoint : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartStoryFlow();
        }
        else
        {
            Debug.LogError("GameManager.Instance is null.");
        }
    }
}