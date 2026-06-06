using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public PlayerCharacter Player { get; private set; }
    public EnemyCharacter CurrentEnemy { get; private set; }
    public Queue<ICombatActor> TurnQueue { get; private set; } = new();

    public event Action OnCombatStarted;
    public event Action<string> OnCombatLog;
    public event Action OnCombatUpdated;
    public event Action<bool> OnCombatEnded;

    public void StartCombat(PlayerCharacter player, EnemyCharacter enemy)
    {
        Player = player;
        CurrentEnemy = enemy;

        BuildTurnQueue();
        OnCombatStarted?.Invoke();
        OnCombatUpdated?.Invoke();
    }

    public void ExecutePlayerAction(CombatActionType actionType)
    {
        if (Player == null || CurrentEnemy == null)
        {
            return;
        }

        CombatActionData playerAction = Player.ChooseAction(actionType);
        ResolveAction(Player, CurrentEnemy, playerAction);

        if (CheckCombatEnd())
        {
            return;
        }

        CombatActionData enemyAction = CurrentEnemy.PerformTurn();
        ResolveAction(CurrentEnemy, Player, enemyAction);

        if (CheckCombatEnd())
        {
            return;
        }

        BuildTurnQueue();
        OnCombatUpdated?.Invoke();
    }

    private void BuildTurnQueue()
    {
        TurnQueue.Clear();

        int playerInit = Player.GetInitiative();
        int enemyInit = CurrentEnemy.GetInitiative();

        if (playerInit >= enemyInit)
        {
            TurnQueue.Enqueue(Player);
            TurnQueue.Enqueue(CurrentEnemy);
        }
        else
        {
            TurnQueue.Enqueue(CurrentEnemy);
            TurnQueue.Enqueue(Player);
        }
    }

    private void ResolveAction(Character source, Character target, CombatActionData action)
    {
        switch (action.ActionType)
        {
            case CombatActionType.Attack:
                int damage = Mathf.Max(1, action.BasePower);
                target.ReceiveDamage(damage);
                OnCombatLog?.Invoke($"{source.Name} dealt {damage} damage to {target.Name}.");
                break;

            case CombatActionType.Defend:
                source.SetDefending(true);
                OnCombatLog?.Invoke($"{source.Name} is defending.");
                break;

            case CombatActionType.UsePotion:
                if (source is PlayerCharacter player && player.TryUsePotion())
                {
                    OnCombatLog?.Invoke($"{source.Name} used a potion.");
                }
                else
                {
                    OnCombatLog?.Invoke($"{source.Name} tried to use a potion, but none was available.");
                }
                break;
        }
    }

    private bool CheckCombatEnd()
    {
        if (!Player.IsAlive())
        {
            OnCombatEnded?.Invoke(false);
            return true;
        }

        if (!CurrentEnemy.IsAlive())
        {
            Player.AddExperience(CurrentEnemy.RewardXP);
            OnCombatEnded?.Invoke(true);
            return true;
        }

        return false;
    }
}