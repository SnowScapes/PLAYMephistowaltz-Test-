using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // �̱��� ������ ������ Ŭ������ ���������� ������ֱ� ���� Ŭ����

    private static T instance;

    public static T Instance
    {
        get
        {
            // �̱��� �ν��Ͻ��� �����Ǿ����� ���� ��� ������Ʈ ���� �� ������Ʈ �߰�
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
        // �̱��� �ν��Ͻ� �Ҵ�
        // ��ӹ��� Ŭ�������� Awake�� ������ �� �� �ֱ� ������ virtual�� ����
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
