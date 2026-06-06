using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingUIController : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private LocalizedTextBinder endingTextBinder;
    [SerializeField] private Button backToMenuButton;
    [SerializeField] private NarrativeManager narrativeManager;

    private void Start()
    {
        backToMenuButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
    }

    private void OnEnable()
    {
        narrativeManager.OnEndingReached += ShowEnding;
    }

    private void OnDisable()
    {
        narrativeManager.OnEndingReached -= ShowEnding;
    }

    private void ShowEnding(StoryNodeData node)
    {
        root.SetActive(true);
        endingTextBinder.SetEntry(node.GetTableName(), node.GetEntryKey());
    }
}