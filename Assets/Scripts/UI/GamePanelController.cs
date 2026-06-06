using UnityEngine;

public class GamePanelController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject narrativePanel;
    [SerializeField] private GameObject combatPanel;
    [SerializeField] private GameObject endingPanel;

    private void Awake()
    {
        HideAll();
    }

    public void ShowNarrative()
    {
        hudPanel.SetActive(true);
        narrativePanel.SetActive(true);
        combatPanel.SetActive(false);
        endingPanel.SetActive(false);
    }

    public void ShowCombat()
    {
        hudPanel.SetActive(true);
        narrativePanel.SetActive(false);
        combatPanel.SetActive(true);
        endingPanel.SetActive(false);
    }

    public void ShowEnding()
    {
        hudPanel.SetActive(false);
        narrativePanel.SetActive(false);
        combatPanel.SetActive(false);
        endingPanel.SetActive(true);
    }

    public void HideAll()
    {
        hudPanel.SetActive(false);
        narrativePanel.SetActive(false);
        combatPanel.SetActive(false);
        endingPanel.SetActive(false);
    }
}