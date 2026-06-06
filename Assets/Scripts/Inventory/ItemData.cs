using UnityEngine;

[CreateAssetMenu(menuName = "Adventure/Items/Item Data")]
public class ItemData : ScriptableObject
{
    public string ItemId;
    public string DisplayNameKey;
    public int HealValue;
}