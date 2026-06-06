using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationManager : MonoBehaviour
{
    public bool IsInitialized { get; private set; }

    private IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;
        IsInitialized = true;
    }

    public void SetLocale(string localeCode)
    {
        if (!IsInitialized)
        {
            return;
        }

        var locales = LocalizationSettings.AvailableLocales.Locales;

        foreach (var locale in locales)
        {
            if (locale.Identifier.Code == localeCode)
            {
                LocalizationSettings.SelectedLocale = locale;
                return;
            }
        }

        Debug.LogWarning($"Locale not found: {localeCode}");
    }

    public string GetCurrentLocaleCode()
    {
        if (LocalizationSettings.SelectedLocale == null)
        {
            return "en";
        }

        return LocalizationSettings.SelectedLocale.Identifier.Code;
    }

    public void ToggleLocale()
    {
        string current = GetCurrentLocaleCode();
        SetLocale(current == "es" ? "en" : "es");
    }
}