using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private readonly IList<GameEventListener> _events = new List<GameEventListener>();

    public void Raise()
    {
        for (var i = _events.Count - 1; i >= 0; i--)
        {
            _events[i].OnEventRaised();
        }
    }

    public void Register(GameEventListener listener)
    {
        _events.Add(listener);
    }

    public void UnRegister(GameEventListener listener)
    {
        _events.Remove(listener);
    }
}
