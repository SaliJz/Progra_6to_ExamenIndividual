using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeUIController : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private LocalizedTextBinder storyTextBinder;
    [SerializeField] private List<Button> choiceButtons;
    [SerializeField] private List<LocalizedTextBinder> choiceBinders;

    [SerializeField] private NarrativeManager narrativeManager;

    private void OnEnable()
    {
        narrativeManager.OnNodeChanged += RefreshNode;
    }

    private void OnDisable()
    {
        narrativeManager.OnNodeChanged -= RefreshNode;
    }

    public void RefreshNode(StoryNodeData node)
    {
        root.SetActive(true);
        storyTextBinder.SetEntry(node.GetTableName(), node.GetEntryKey());

        for (int i = 0; i < choiceButtons.Count; i++)
        {
            bool active = i < node.Choices.Count;
            choiceButtons[i].gameObject.SetActive(active);

            if (!active)
            {
                continue;
            }

            int index = i;
            ChoiceData choice = node.Choices[i];

            choiceBinders[i].SetEntry(choice.GetTableName(), choice.GetEntryKey());
            choiceButtons[i].onClick.RemoveAllListeners();
            choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(index));
            choiceButtons[i].interactable = choice.IsAvailable(GameManager.Instance.Player);
        }
    }

    private void OnChoiceSelected(int index)
    {
        narrativeManager.SelectChoice(index, GameManager.Instance.Player);
    }

    public void Hide()
    {
        root.SetActive(false);
    }
}