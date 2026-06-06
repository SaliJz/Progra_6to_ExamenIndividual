using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterStats
{
    [SerializeField] private List<StatEntry> serializedStats = new();

    private Dictionary<StatType, int> values = new();

    [Serializable]
    private class StatEntry
    {
        public StatType Type;
        public int Value;
    }

    public void InitializeDefaults(int strength, int dexterity, int constitution, int intelligence, int wisdom, int charisma)
    {
        values = new Dictionary<StatType, int>
        {
            { StatType.Strength, strength },
            { StatType.Dexterity, dexterity },
            { StatType.Constitution, constitution },
            { StatType.Intelligence, intelligence },
            { StatType.Wisdom, wisdom },
            { StatType.Charisma, charisma }
        };

        SyncSerializedList();
    }

    public void BuildDictionary()
    {
        values.Clear();

        foreach (var entry in serializedStats)
        {
            if (!values.ContainsKey(entry.Type))
            {
                values.Add(entry.Type, entry.Value);
            }
        }
    }

    public int GetStat(StatType type)
    {
        if (values.Count == 0)
        {
            BuildDictionary();
        }

        return values.TryGetValue(type, out int value) ? value : 10;
    }

    public void SetStat(StatType type, int value)
    {
        if (values.Count == 0)
        {
            BuildDictionary();
        }

        values[type] = value;
        SyncSerializedList();
    }

    public void AddStat(StatType type, int amount)
    {
        int current = GetStat(type);
        SetStat(type, current + amount);
    }

    public int GetModifier(StatType type)
    {
        int statValue = GetStat(type);
        return Mathf.FloorToInt((statValue - 10) / 2f);
    }

    public int GetMaxHP()
    {
        return 10 + Mathf.Max(0, GetModifier(StatType.Constitution)) * 2;
    }

    private void SyncSerializedList()
    {
        serializedStats.Clear();

        foreach (var pair in values)
        {
            serializedStats.Add(new StatEntry
            {
                Type = pair.Key,
                Value = pair.Value
            });
        }
    }
}