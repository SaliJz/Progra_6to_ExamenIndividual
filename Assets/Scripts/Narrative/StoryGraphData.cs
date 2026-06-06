using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Adventure/Narrative/Story Graph Data")]
public class StoryGraphData : ScriptableObject
{
    public string GraphId;
    public string StartNodeId;
    public List<StoryNodeData> Nodes = new();

    private Dictionary<string, StoryNodeData> nodeIndex = new();

    public void BuildIndex()
    {
        nodeIndex.Clear();

        foreach (var node in Nodes)
        {
            if (!string.IsNullOrWhiteSpace(node.NodeId) && !nodeIndex.ContainsKey(node.NodeId))
            {
                nodeIndex.Add(node.NodeId, node);
            }
        }
    }

    public StoryNodeData GetNodeById(string nodeId)
    {
        if (nodeIndex.Count == 0)
        {
            BuildIndex();
        }

        return nodeIndex.TryGetValue(nodeId, out StoryNodeData node) ? node : null;
    }
}