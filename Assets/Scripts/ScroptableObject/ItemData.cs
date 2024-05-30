using UnityEngine;

//아이템 타입
public enum ItemType
{
    Buff,
    Consumable
}

//회복되는 타입
public enum ConsumableType
{
    Hunger,
    Speed
}

//데이터
[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

//스크립터블오브젝트
[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable consumable;
}