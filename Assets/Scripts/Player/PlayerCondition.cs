using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uICondition;

    public Condition health { get { return uICondition.health; } }
    public Condition hunger { get { return uICondition.hunger; } }

    public float noHungerHealthDecay;

    void Update()
    {
        hunger.AddValue(hunger.passiveValue * Time.deltaTime);

        if (hunger.curValue <= 0.0f) health.AddValue(noHungerHealthDecay * Time.deltaTime);
        else health.AddValue(health.passiveValue * Time.deltaTime);

        if (health.curValue <= 0.0f) Die();
    }

    public void Die()
    {
        Debug.Log("ав╬З╢ы");
    }
}
