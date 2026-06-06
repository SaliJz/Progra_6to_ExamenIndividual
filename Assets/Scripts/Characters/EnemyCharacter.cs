using UnityEngine;

public class EnemyCharacter : Character
{
    public string EnemyId { get; private set; }
    public int RewardXP { get; private set; }

    public EnemyCharacter(string enemyId, string name, int level, CharacterStats stats, int rewardXP) : base(name, level, stats)
    {
        EnemyId = enemyId;
        RewardXP = rewardXP;
    }

    public override CombatActionData PerformTurn()
    {
        int roll = Random.Range(0, 100);

        if (roll < 75)
        {
            return new CombatActionData(CombatActionType.Attack, GetAttackPower(), Name);
        }

        return new CombatActionData(CombatActionType.Defend, 0, Name);
    }
}