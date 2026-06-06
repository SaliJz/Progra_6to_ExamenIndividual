using UnityEngine;

public abstract class Character : IDamageable, ICombatActor
{
    public string Name { get; protected set; }
    public int Level { get; protected set; }
    public int CurrentHP { get; protected set; }
    public int MaxHP { get; protected set; }
    public bool IsDefending { get; protected set; }
    public CharacterStats Stats { get; protected set; }

    protected Character(string name, int level, CharacterStats stats)
    {
        Name = name;
        Level = level;
        Stats = stats;
        MaxHP = stats.GetMaxHP();
        CurrentHP = MaxHP;
        IsDefending = false;
    }

    public virtual void ReceiveDamage(int amount)
    {
        int finalDamage = amount;

        if (IsDefending)
        {
            finalDamage = Mathf.Max(1, amount / 2);
        }

        CurrentHP = Mathf.Max(0, CurrentHP - finalDamage);
        IsDefending = false;
    }

    public virtual void Heal(int amount)
    {
        CurrentHP = Mathf.Min(MaxHP, CurrentHP + amount);
    }

    public bool IsAlive()
    {
        return CurrentHP > 0;
    }

    public virtual int GetInitiative()
    {
        return Random.Range(1, 21) + Stats.GetModifier(StatType.Dexterity);
    }

    public void SetDefending(bool value)
    {
        IsDefending = value;
    }

    public int GetAttackPower()
    {
        return Mathf.Max(1, 3 + Stats.GetModifier(StatType.Strength));
    }

    public abstract CombatActionData PerformTurn();
}