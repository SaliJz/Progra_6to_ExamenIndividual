using System;

[Serializable]
public class ChoiceRequirementData
{
    public bool HasRequirement;
    public StatType RequiredStat;
    public int MinimumValue;

    public bool IsMet(PlayerCharacter player)
    {
        if (!HasRequirement)
        {
            return true;
        }

        return player.Stats.GetStat(RequiredStat) >= MinimumValue;
    }
}