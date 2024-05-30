using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;
    public float startValue;
    public float maxValue;
    public float passiveValue;
    public Image uiBar;

    void Start()
    {
        curValue = startValue;
    }

    void Update()
    {
        //ui 업데이트
        uiBar.fillAmount = GetPercentage();
    }

    float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void AddValue(float value)
    {
        curValue = Mathf.Clamp(curValue + value, 0, maxValue);
    }
}
