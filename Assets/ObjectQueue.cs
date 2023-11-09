using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectQueue : ScriptableObject, ISerializationCallbackReceiver
{
    [NonSerialized]
    private Queue<GameObject> queue;

    public void OnBeforeSerialize()
    {

    }

    public void Enqueue(GameObject gameObject)
    {
        queue.Enqueue(gameObject);
    }

    public GameObject Dequeue()
    {
        return queue.Dequeue();
    }

    public void OnAfterDeserialize()
    {
        queue = new Queue<GameObject>();
    }
}