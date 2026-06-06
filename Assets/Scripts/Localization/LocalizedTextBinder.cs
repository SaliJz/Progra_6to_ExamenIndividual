using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LocalizedTextBinder : MonoBehaviour
{
    [SerializeField] private TMP_Text targetText;
    [SerializeField] private string tableName = "UI";
    [SerializeField] private string entryKey;

    private void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
        RefreshText();
    }

    private void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
    }

    private void OnLocaleChanged(Locale locale)
    {
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

        LocalizedString localizedString = new LocalizedString
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
            handle.Completed += op =>
            {
                if (op.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
                {
                    targetText.text = op.Result;
                }
            };
        }
    }
}