using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickSystem : MonoBehaviour
{
    private float _tickTimerMax = 1.0f;
    private float _tickTimer;

    [SerializeField]
    private GameEvent TickEvent;

    void Update()
    {
        _tickTimer += Time.deltaTime;

        if(_tickTimer >= _tickTimerMax)
        {
            _tickTimer -= _tickTimerMax;
            TickEvent.Raise();
        }
    }
}
