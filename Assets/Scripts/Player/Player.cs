using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public PlayerCondition condition;
    public PlayerController controller;

    public ItemData itemData;
    public Action addItem;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }

    private void Start()
    {

    }
}
