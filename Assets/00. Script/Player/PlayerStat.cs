using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour, IDamage
{
    [Header("초기 스탯 정보")]
    [SerializeField] private float initMoveSpeed;
    [SerializeField] private int initHealth;

    [Header("현재 스탯 정보")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private int health;

    private SaveData saveData => GameManager.Instance.SaveData;
    public float MoveSpeed => moveSpeed;
    public float Health => health;

    private void Start()
    {
        moveSpeed = saveData != null ? saveData.MoveSpeed : initMoveSpeed;
        health = saveData != null ? saveData.Health : initHealth;
    }

    public void HandleDamage(int damage)
    {
        health -= damage;
    }
}
