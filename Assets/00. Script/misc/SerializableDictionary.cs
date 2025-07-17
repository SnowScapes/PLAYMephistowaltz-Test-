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