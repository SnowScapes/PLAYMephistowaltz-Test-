# 플레이 메피스토왈츠 실기 테스트

<details>
  <summary>
    <b>1. Singleton<T></b> 으로 사용 될 클래스를 위한 Generic 클래스
  </summary>

  ```csharp
  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // 싱글톤 패턴을 적용할 클래스에 공통적으로 상속해주기 위한 클래스

    private static T instance;

    public static T Instance
    {
        get
        {
            // 싱글톤 인스턴스가 생성되어있지 않은 경우 오브젝트 생성 및 컴포넌트 추가
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    instance = go.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    protected virtual void Awake() 
    {
        // 싱글톤 인스턴스 할당
        // 상속받은 클래스에서 Awake를 재정의 할 수 있기 때문에 virtual로 선언
        if (instance == null) 
        {
            instance = gameObject.GetComponent<T>();

            if (instance == null)
                gameObject.AddComponent<T>();

            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }
}

  ```
</details>

<details>
  <summary>
    <b>2. </b>모든 풀링 오브젝트가 관리되는 하나의 <b>ObjectPool</b>
  </summary>

  ```csharp
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
  ```
</details>

<details>
  <summary>
    <b>3. Dictionary 직렬화</b>를 위한 SerializableDictionary
  </summary>

  ```csharp
  using System;
using System.Collections.Generic;
using UnityEngine;

// 기본 Unity Inspector에서 KeyValuePair가 직렬화 되지 않아 만든 클래스
[Serializable]
public class SerializableKeyValue<K, V>
{
    public K Key;
    public V Value;
}

// 기본 Unity Inspector에서 Dictionary가 직렬화 되지 않아 만든 클래스
[Serializable]
public class SerializableDictionary<K, V> : Dictionary<K, V>, ISerializationCallbackReceiver
{
    [SerializeField] private List<SerializableKeyValue<K, V>> _keyValueList;

    public void OnBeforeSerialize()
    {
        if (this.Count < _keyValueList.Count)
        {
            return;
        }

        _keyValueList.Clear();

        foreach (var kv in this)
        {
            _keyValueList.Add(new SerializableKeyValue<K, V>()
            {
                Key = kv.Key,
                Value = kv.Value
            });
        }
    }

    public void OnAfterDeserialize()
    {
        this.Clear();
        // 중복된 키가 있을 경우 에러 발생
        foreach (var kv in _keyValueList)
        {
            if (!this.TryAdd(kv.Key, kv.Value))
            {
                Debug.LogError($"List has duplicate Key : {kv.Key}");
            }
        }
    }
}
  ```
</details>

<details>
  <summary>
    <b>4. InputSystem의 Event</b>로 관리되는 Input
  </summary>

  ```csharp
  private PlayerInput input => InputManager.Instance.Input;

  // InputAction의 각 Action에 실행될 메서드 구독 및 Input 활성화
private void OnEnable()
{
    input.Player.Move.performed += OnMove;
    input.Player.Move.canceled += OnMove;
    input.Player.Look.performed += OnLook;
    input.Player.Shoot.performed += OnShoot;
    input.Player.Enable();
}

// InputAction의 각 Action에 실행될 메서드 구독해제 및 Input 비활성화
private void OnDisable()
{
    input.Player.Move.performed -= OnMove;
    input.Player.Move.canceled -= OnMove;
    input.Player.Look.performed -= OnLook;
    input.Player.Shoot.performed -= OnShoot;
    input.Player.Disable();
}

private void OnEnable()
{
    input.Player.Move.performed += MoveAnimation;
    input.Player.Move.canceled += MoveAnimation;
}

private void OnDisable()
{
    input.Player.Move.performed -= MoveAnimation;
    input.Player.Move.canceled -= MoveAnimation;
}
  ```
</details>

<b>5. 전략패턴을 활용하여 데미지 처리를 위한 IDamage</b>
