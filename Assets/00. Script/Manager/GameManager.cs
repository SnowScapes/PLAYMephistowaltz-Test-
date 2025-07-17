using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("세이브 데이터")]
    [SerializeField] private SaveData saveData;

    [Header("게임 정보")]
    [SerializeField] private int level;
    [SerializeField] private int score;

    public int Level => level;
    public int Score => score;
    public SaveData SaveData => saveData;

    private void Start()
    {
        // 세이브 데이터 불러오기
        // 데이터가 없으면 0으로 초기화, 있다면 데이터 불러오기
        level = saveData != null ? saveData.Level : 0;
        score = saveData != null ? saveData.Score : 0;
    }
}
