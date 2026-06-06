using NUnit.Framework.Interfaces;
using System.Collections.Generic;

public class PlayerCharacter : Character
{
    public int Experience { get; private set; }
    public List<ItemData> Inventory { get; private set; }

    public PlayerCharacter(string name, int level, CharacterStats stats) : base(name, level, stats)
    {
        Experience = 0;
        Inventory = new List<ItemData>();
    }

    public override CombatActionData PerformTurn()
    {
        return new CombatActionData(CombatActionType.Attack, GetAttackPower(), Name);
    }

    public CombatActionData ChooseAction(CombatActionType actionType)
    {
        return actionType switch
        {
            CombatActionType.Attack => new CombatActionData(CombatActionType.Attack, GetAttackPower(), Name),
            CombatActionType.Defend => new CombatActionData(CombatActionType.Defend, 0, Name),
            CombatActionType.UsePotion => new CombatActionData(CombatActionType.UsePotion, 5, Name),
            _ => new CombatActionData(CombatActionType.Attack, GetAttackPower(), Name)
        };
    }

    public void ApplyChoiceEffect(ChoiceEffectData effect)
    {
        if (effect == null)
        {
            return;
        }

        foreach (var statChange in effect.StatChanges)
        {
            Stats.AddStat(statChange.Key, statChange.Value);
        }

        if (effect.HealAmount > 0)
        {
            Heal(effect.HealAmount);
        }

        if (effect.DamageAmount > 0)
        {
            ReceiveDamage(effect.DamageAmount);
        }

        MaxHP = Stats.GetMaxHP();
        CurrentHP = CurrentHP > MaxHP ? MaxHP : CurrentHP;
    }

    public void AddExperience(int amount)
    {
        Experience += amount;
    }

    public bool TryUsePotion()
    {
        ItemData potion = Inventory.Find(i => i.HealValue > 0);

        if (potion == null)
        {
            return false;
        }

        Heal(potion.HealValue);
        Inventory.Remove(potion);
        return true;
    }
}