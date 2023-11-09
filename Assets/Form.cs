using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Form : MonoBehaviour
{
    [SerializeField]
    private Block _Prefab;
    [SerializeField]
    private ListVariable _grid;
    [SerializeField]
    private BlockBuilder _blockBuilder;

    private List<Block> _childs;

    private float _tickTimer = 0;
    private const float _tickTimerMax = 0.5f;
    private const float _lowestTickTimer = 0.1f;
    private const float _tickTimerSteps = 0.1f;
    private float _localTickTimer;

    private Vector3 _moveDirection;
    private bool _isMoving = false;



    public void Start()
    {
        _localTickTimer = _tickTimerMax;
        _childs = _blockBuilder.GetNewForm(transform);
    }

    public void Update()
    {
        if(!_isMoving)
        {
            return;
        }
        _tickTimer += Time.deltaTime;
        if(_tickTimer >= _localTickTimer)
        {
            _tickTimer -= _localTickTimer;
            _localTickTimer -= _tickTimerSteps;
            if(_localTickTimer < _lowestTickTimer)
            {
                _localTickTimer = _lowestTickTimer;
            }
            Move(_moveDirection);
        }
    }
    
    //constantly moves the form down
    public void MoveDown()
    {
        Move(Vector2.down);
    }


    //starts the movement when button is pressed, cancels it when it is released
    public void OnMoveInput(Vector2 direction)
    {
        _tickTimer = 0;
        _isMoving = false;
        print("Ok");
        if(direction != default)
        {
            print("NOOO");
            _moveDirection = direction;
            _isMoving = true;
            _localTickTimer = _tickTimerMax;
            Move(direction);
        }
    }

    public void Move(Vector2 direction)
    {
        if (CheckMove(direction))
        {
            transform.position += (Vector3)direction;
        }
        else if (direction == Vector2.down)
        {
            Reset();
        }
    }

    public void Reset()
    {
        _localTickTimer = _tickTimerMax;
        _tickTimer = 0;
        _grid.Set(_childs);
        _childs.Clear();
        transform.DetachChildren();
        transform.localPosition = new Vector3(0, 6);
        _childs = _blockBuilder.GetNewForm(transform);
    }

    public bool CheckMove(Vector2 direction)
    {
        foreach (var child in _childs)
        {
            if (!child.CheckValidMove((Vector2)child.transform.position + direction))
            {
                return false;
            }
        }
        return true;
    }

    public void OnRotate(InputValue value)
    {
        List<Vector2> swaps = new List<Vector2>();
        foreach (var child in _childs)
        {
            Vector2 relativeSwapPosition = child.Rotate((int)value.Get<float>());
            if(child.CheckValidMove((Vector2)transform.position + relativeSwapPosition))
            {
                swaps.Add(relativeSwapPosition);
                continue;
            }
            return;
        }
        for(var i = 0; i<4; i++)
        {
            _childs[i].transform.localPosition = swaps[i];
        }
    }
}
