using TMPro;
using UnityEngine;

public class HUDUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text statsText;

    private void Update()
    {
        if (GameManager.Instance == null || GameManager.Instance.Player == null)
        {
            return;
        }

        PlayerCharacter player = GameManager.Instance.Player;
        playerNameText.text = player.Name;
        hpText.text = $"HP: {player.CurrentHP}/{player.MaxHP}";
        statsText.text =
            $"STR {player.Stats.GetStat(StatType.Strength)}  " +
            $"DEX {player.Stats.GetStat(StatType.Dexterity)}  " +
            $"CON {player.Stats.GetStat(StatType.Constitution)}";
    }
}