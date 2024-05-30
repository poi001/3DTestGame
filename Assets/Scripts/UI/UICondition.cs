using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;
    public Condition hunger;

    void Start()
    {
        CharacterManager.Instance.Player.condition.uICondition = this;
    }
}
