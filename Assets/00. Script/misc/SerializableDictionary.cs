using System;
using System.Collections.Generic;
using UnityEngine;

// �⺻ Unity Inspector���� KeyValuePair�� ����ȭ ���� �ʾ� ���� Ŭ����
[Serializable]
public class SerializableKeyValue<K, V>
{
    public K Key;
    public V Value;
}

// �⺻ Unity Inspector���� Dictionary�� ����ȭ ���� �ʾ� ���� Ŭ����
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
        // �ߺ��� Ű�� ���� ��� ���� �߻�
        foreach (var kv in _keyValueList)
        {
            if (!this.TryAdd(kv.Key, kv.Value))
            {
                Debug.LogError($"List has duplicate Key : {kv.Key}");
            }
        }
    }
}