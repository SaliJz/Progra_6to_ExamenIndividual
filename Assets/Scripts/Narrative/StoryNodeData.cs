using System;
using System.Collections.Generic;

[Serializable]
public class StoryNodeData : ILocalizedContent
{
    public string NodeId;
    public NodeType NodeType;
    public string TextKey;
    public List<ChoiceData> Choices = new();
    public string CombatEncounterAddress;
    public bool IsFinalNode;

    public string GetTableName()
    {
        return "Story";
    }

    public string GetEntryKey()
    {
        return TextKey;
    }
}