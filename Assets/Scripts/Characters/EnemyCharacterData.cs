using UnityEngine;

[CreateAssetMenu(menuName = "Adventure/Characters/Enemy Data")]
public class EnemyCharacterData : ScriptableObject
{
    public string EnemyId;
    public string DisplayName;
    public int Level = 1;
    public int RewardXP = 5;

    [Header("Stats")]
    public int Strength = 10;
    public int Dexterity = 10;
    public int Constitution = 10;
    public int Intelligence = 10;
    public int Wisdom = 10;
    public int Charisma = 10;

    public EnemyCharacter CreateRuntimeEnemy()
    {
        CharacterStats stats = new CharacterStats();
        stats.InitializeDefaults(Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma);

        return new EnemyCharacter(EnemyId, DisplayName, Level, stats, RewardXP);
    }
}