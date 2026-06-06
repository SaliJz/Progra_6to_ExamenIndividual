public class CombatActionData
{
    public CombatActionType ActionType { get; private set; }
    public int BasePower { get; private set; }
    public string SourceId { get; private set; }

    public CombatActionData(CombatActionType actionType, int basePower, string sourceId)
    {
        ActionType = actionType;
        BasePower = basePower;
        SourceId = sourceId;
    }
}