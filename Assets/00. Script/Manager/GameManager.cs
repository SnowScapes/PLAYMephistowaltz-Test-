using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("���̺� ������")]
    [SerializeField] private SaveData saveData;

    [Header("���� ����")]
    [SerializeField] private int level;
    [SerializeField] private int score;

    public int Level => level;
    public int Score => score;
    public SaveData SaveData => saveData;

    private void Start()
    {
        // ���̺� ������ �ҷ�����
        // �����Ͱ� ������ 0���� �ʱ�ȭ, �ִٸ� ������ �ҷ�����
        level = saveData != null ? saveData.Level : 0;
        score = saveData != null ? saveData.Score : 0;
    }
}
