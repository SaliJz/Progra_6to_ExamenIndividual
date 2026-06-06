using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LocalizedTextBinder : MonoBehaviour
{
    [SerializeField] private TMP_Text targetText;
    [SerializeField] private string tableName = "UI";
    [SerializeField] private string entryKey;

    private LocalizedString localizedString;

    private void OnEnable()
    {
        localizedString = new LocalizedString
        {
            TableReference = tableName,
            TableEntryReference = entryKey
        };

        RefreshText();
    }

    public void SetEntry(string newTableName, string newEntryKey)
    {
        tableName = newTableName;
        entryKey = newEntryKey;
        RefreshText();
    }

    public void RefreshText()
    {
        if (targetText == null || string.IsNullOrWhiteSpace(entryKey))
        {
            return;
        }

        localizedString = new LocalizedString
        {
            TableReference = tableName,
            TableEntryReference = entryKey
        };

        AsyncOperationHandle<string> handle = localizedString.GetLocalizedStringAsync();

        if (handle.IsDone)
        {
            targetText.text = handle.Result;
        }
        else
        {
            handle.Completed += OnStringLoaded;
        }
    }

    private void OnStringLoaded(AsyncOperationHandle<string> handle)
    {
        targetText.text = handle.Result;
    }
}