using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private List<GameObject> enemyList;
    [SerializeField] private int enemySpawnCount;
    [SerializeField] private int enemySpawnIntervalTime;

    private WaitForSecondsRealtime spawnInterval;
    private Coroutine enemySpawnCoroutine;

    void Start()
    {
        // 코루틴에서 IEnumerator 반환시 반복되는 new를 막기 위해 인스턴스 생성
        spawnInterval = new(enemySpawnIntervalTime);

        // 오브젝트풀에게 적 오브젝트 풀링 요청
        ObjectPool.Instance.PoolObject(ObjectType.Monster, 20);
    }

    private void StartSpawn()
    {
        // 적 스폰 코루틴이 이미 동작중인 경우 정지 및 적 오브젝트 반환 후 재시작
        // (게임이 재시작 되었을 경우를 위함)
        if (enemySpawnCoroutine != null)
        {
            StopCoroutine(enemySpawnCoroutine);
            enemySpawnCoroutine = null;

            for (int i = 0; i < enemyList.Count; i++)
            {
                ObjectPool.Instance.ReleaseObject(enemyList[i]);
            }
        }

        // 적 스폰 코루틴 시작
        enemySpawnCoroutine = StartCoroutine(EnemySpawn());
    }

    private IEnumerator EnemySpawn()
    {
        // spawnIntervalTime 시간마다 최대 Enemy 수보다 현재 Enemy 수가 적을 시 스폰
        while (true)
        {
            if (enemyList.Count < enemySpawnCount)
                ObjectPool.Instance.GetObject(ObjectType.Monster, transform);

            yield return spawnInterval;
        }
    }
}
