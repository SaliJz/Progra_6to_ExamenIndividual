using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatUIController : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private TMP_Text playerHpText;
    [SerializeField] private TMP_Text enemyHpText;
    [SerializeField] private TMP_Text combatLogText;
    [SerializeField] private Button attackButton;
    [SerializeField] private Button defendButton;
    [SerializeField] private Button potionButton;

    [SerializeField] private CombatManager combatManager;

    private void Start()
    {
        attackButton.onClick.AddListener(() => combatManager.ExecutePlayerAction(CombatActionType.Attack));
        defendButton.onClick.AddListener(() => combatManager.ExecutePlayerAction(CombatActionType.Defend));
        potionButton.onClick.AddListener(() => combatManager.ExecutePlayerAction(CombatActionType.UsePotion));
    }

    private void OnEnable()
    {
        combatManager.OnCombatStarted += Show;
        combatManager.OnCombatUpdated += Refresh;
        combatManager.OnCombatLog += SetLog;
        combatManager.OnCombatEnded += Hide;
    }

    private void OnDisable()
    {
        combatManager.OnCombatStarted -= Show;
        combatManager.OnCombatUpdated -= Refresh;
        combatManager.OnCombatLog -= SetLog;
        combatManager.OnCombatEnded -= Hide;
    }

    private void Show()
    {
        root.SetActive(true);
        Refresh();
    }

    private void Refresh()
    {
        if (combatManager.Player == null || combatManager.CurrentEnemy == null)
        {
            return;
        }

        playerHpText.text = $"HP: {combatManager.Player.CurrentHP}/{combatManager.Player.MaxHP}";
        enemyHpText.text = $"Enemy HP: {combatManager.CurrentEnemy.CurrentHP}/{combatManager.CurrentEnemy.MaxHP}";
    }

    private void SetLog(string logText)
    {
        combatLogText.text = logText;
    }

    private void Hide(bool playerWon)
    {
        root.SetActive(false);
    }
}