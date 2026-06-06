using System;
using System.Collections.Generic;

[Serializable]
public class ChoiceData : ILocalizedContent
{
    public string ChoiceId;
    public string TextKey;
    public string TargetNodeId;
    public List<ChoiceEffectData> Effects = new();
    public ChoiceRequirementData Requirement;

    public string GetTableName()
    {
        return "Story";
    }

    public string GetEntryKey()
    {
        return TextKey;
    }

    public bool IsAvailable(PlayerCharacter player)
    {
        if (Requirement == null)
        {
            return true;
        }

        return Requirement.IsMet(player);
    }
}