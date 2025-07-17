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
        // �ڷ�ƾ���� IEnumerator ��ȯ�� �ݺ��Ǵ� new�� ���� ���� �ν��Ͻ� ����
        spawnInterval = new(enemySpawnIntervalTime);

        // ������ƮǮ���� �� ������Ʈ Ǯ�� ��û
        ObjectPool.Instance.PoolObject(ObjectType.Monster, 20);
    }

    private void StartSpawn()
    {
        // �� ���� �ڷ�ƾ�� �̹� �������� ��� ���� �� �� ������Ʈ ��ȯ �� �����
        // (������ ����� �Ǿ��� ��츦 ����)
        if (enemySpawnCoroutine != null)
        {
            StopCoroutine(enemySpawnCoroutine);
            enemySpawnCoroutine = null;

            for (int i = 0; i < enemyList.Count; i++)
            {
                ObjectPool.Instance.ReleaseObject(enemyList[i]);
            }
        }

        // �� ���� �ڷ�ƾ ����
        enemySpawnCoroutine = StartCoroutine(EnemySpawn());
    }

    private IEnumerator EnemySpawn()
    {
        // spawnIntervalTime �ð����� �ִ� Enemy ������ ���� Enemy ���� ���� �� ����
        while (true)
        {
            if (enemyList.Count < enemySpawnCount)
                ObjectPool.Instance.GetObject(ObjectType.Monster, transform);

            yield return spawnInterval;
        }
    }
}
