using System;
using System.Collections.Generic;

[Serializable]
public class ChoiceEffectData
{
    public List<StatChangeData> SerializedStatChanges = new();
    public int HealAmount;
    public int DamageAmount;

    public Dictionary<StatType, int> StatChanges
    {
        get
        {
            Dictionary<StatType, int> result = new();

            foreach (var item in SerializedStatChanges)
            {
                if (!result.ContainsKey(item.StatType))
                {
                    result.Add(item.StatType, item.Amount);
                }
            }

            return result;
        }
    }
}

[Serializable]
public class StatChangeData
{
    public StatType StatType;
    public int Amount;
}