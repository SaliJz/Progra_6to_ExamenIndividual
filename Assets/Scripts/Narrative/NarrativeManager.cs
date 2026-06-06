using System;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeManager : MonoBehaviour
{
    public StoryGraphData LoadedGraph { get; private set; }
    public StoryNodeData CurrentNode { get; private set; }
    public Stack<StoryNodeData> NodeHistory { get; private set; } = new();

    public event Action<StoryNodeData> OnNodeChanged;
    public event Action<string> OnCombatRequested;
    public event Action<StoryNodeData> OnEndingReached;

    public void LoadGraph(StoryGraphData graph)
    {
        LoadedGraph = graph;
        LoadedGraph.BuildIndex();
    }

    public void StartStory()
    {
        if (LoadedGraph == null)
        {
            Debug.LogError("Story graph not loaded.");
            return;
        }

        CurrentNode = LoadedGraph.GetNodeById(LoadedGraph.StartNodeId);
        NodeHistory.Clear();
        HandleCurrentNode();
    }

    public void SelectChoice(int index, PlayerCharacter player)
    {
        if (CurrentNode == null)
        {
            return;
        }

        if (index < 0 || index >= CurrentNode.Choices.Count)
        {
            return;
        }

        ChoiceData choice = CurrentNode.Choices[index];

        if (!choice.IsAvailable(player))
        {
            Debug.LogWarning("Choice requirement not met.");
            return;
        }

        foreach (var effect in choice.Effects)
        {
            player.ApplyChoiceEffect(effect);
        }

        NodeHistory.Push(CurrentNode);
        CurrentNode = LoadedGraph.GetNodeById(choice.TargetNodeId);
        HandleCurrentNode();
    }

    private void HandleCurrentNode()
    {
        if (CurrentNode == null)
        {
            Debug.LogError("Current node is null.");
            return;
        }

        if (CurrentNode.NodeType == NodeType.Combat && !string.IsNullOrWhiteSpace(CurrentNode.CombatEncounterAddress))
        {
            OnCombatRequested?.Invoke(CurrentNode.CombatEncounterAddress);
            return;
        }

        if (CurrentNode.IsFinalNode || CurrentNode.NodeType == NodeType.Ending)
        {
            OnEndingReached?.Invoke(CurrentNode);
            return;
        }

        OnNodeChanged?.Invoke(CurrentNode);
    }

    public void ResumeAfterCombat()
    {
        OnNodeChanged?.Invoke(CurrentNode);
    }

    public void ForceEnding(StoryNodeData endingNode)
    {
        OnEndingReached?.Invoke(endingNode);
    }
}