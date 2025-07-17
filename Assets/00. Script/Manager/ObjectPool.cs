using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    [SerializeField] private SerializableDictionary<ObjectType, GameObject> objectList;
    private Queue<GameObject> monsters = new();


    // ������Ʈ�� Pool�� ����ϱ� ���� �޼���
    public void PoolObject(ObjectType type, int count)
    {
        for (int i = 0; i < count; i++)
        {
            monsters.Enqueue(Instantiate(objectList[type], transform));
        }
       
    }
    
    // ������Ʈ�� Pool���� �������� ���� Ŭ����
    public GameObject GetObject(ObjectType type, Transform parent)
    {
        if (monsters.TryDequeue(out GameObject go)) 
        {
            return go;
        }
        else
        {
            // ���� Ǯ�� �����ִ� ������Ʈ�� ���� ��� �߰� �����Ͽ� ������
            return Instantiate(objectList[type], parent);
        }

    }

    // ������Ʈ�� Pool�� ��ȯ�ϱ� ���� Ŭ����
    public void ReleaseObject(GameObject go)
    {
        go.SetActive(false);
        monsters.Enqueue(go);
        go.transform.parent = transform;
    }
}
