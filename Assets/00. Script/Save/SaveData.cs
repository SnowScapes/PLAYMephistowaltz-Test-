using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    [Header("플레이어 데이터")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private int health;

    [Header("게임 데이터")]
    [SerializeField] private int level;
    [SerializeField] private int score;

    public float MoveSpeed => moveSpeed;
    public int Health => health;
    public int Level => level;
    public int Score => score;

    public SaveData(float moveSpeed, int health, int level, int score)
    {
        this.moveSpeed = moveSpeed;
        this.health = health;
        this.level = level;
        this.score = score;
    }
}
