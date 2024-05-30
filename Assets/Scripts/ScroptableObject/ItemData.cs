using UnityEngine;

//������ Ÿ��
public enum ItemType
{
    Buff,
    Consumable
}

//ȸ���Ǵ� Ÿ��
public enum ConsumableType
{
    Hunger,
    Speed
}

//������
[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

//��ũ���ͺ������Ʈ
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