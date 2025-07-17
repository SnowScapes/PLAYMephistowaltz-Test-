# 플레이 메피스토왈츠 실기 테스트

<details>
  <summary>
    <b>Singleton\<T\></b> 싱글톤으로 사용 될 클래스를 위한 Generic 클래스
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
