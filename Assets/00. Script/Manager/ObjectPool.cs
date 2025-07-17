using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    [SerializeField] private SerializableDictionary<ObjectType, GameObject> objectList;
    private Queue<GameObject> monsters = new();


    // 오브젝트를 Pool에 등록하기 위한 메서드
    public void PoolObject(ObjectType type, int count)
    {
        for (int i = 0; i < count; i++)
        {
            monsters.Enqueue(Instantiate(objectList[type], transform));
        }
       
    }
    
    // 오브젝트를 Pool에서 가져오기 위한 클래스
    public GameObject GetObject(ObjectType type, Transform parent)
    {
        if (monsters.TryDequeue(out GameObject go)) 
        {
            return go;
        }
        else
        {
            // 현재 풀에 남아있는 오브젝트가 없을 경우 추가 생성하여 내보냄
            return Instantiate(objectList[type], parent);
        }

    }

    // 오브젝트를 Pool에 반환하기 위한 클래스
    public void ReleaseObject(GameObject go)
    {
        go.SetActive(false);
        monsters.Enqueue(go);
        go.transform.parent = transform;
    }
}
