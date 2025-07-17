using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    [Header("�÷��̾� ������")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private int health;

    [Header("���� ������")]
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
