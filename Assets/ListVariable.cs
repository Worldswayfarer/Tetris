using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class ListVariable : ScriptableObject
{
    private int _x = 10, _y = 20;
    [SerializeField]
    private GameObject[,] _gameObjects;
    [SerializeField]
    ObjectQueue _blockPool;

    public ListVariable()
    {
        _gameObjects = new GameObject[_x, _y];
    }

    public bool Get(int x, int y)
    {
        x += _x / 2;
        y += _y / 2;
        if (x < 0 || y < 0 || x >= _x || y >= _y)
        {
            return false;
        }
        if (_gameObjects[x,y] is null)
        {
            return true;
        }
        return false;
    }

    private bool CheckRow(int row)
    {
        for(int x=0; x<_x; x++)
        {
            if (_gameObjects[x,row] == null)
            {
                return false;
            }
        }
        return true;
    }
	
	//enter the current form in the grid
    public void Set(List<Block> newBlocks)
    {
        List<int> rowsToClear= new List<int>();
        foreach (var newBlock in newBlocks)
        {
            Vector2 position = newBlock.transform.position;
            int x = (int)position.x + _x / 2;
            int y = (int)position.y + _y / 2;
            _gameObjects[x, y] = newBlock.GameObject();
            if(rowsToClear.Contains(y))
            {
                continue;
            }
            if(CheckRow(y))
            {
                rowsToClear.Add(y);
            }
        }
        if(rowsToClear?.Any() != true)
        {
            return;
        }
        rowsToClear.Sort((a, b) => b.CompareTo(a));
        ClearRows(rowsToClear);
    }

    public void ClearRows(List<int> rowsToClear)
    {
		//delete Full rows
        foreach(int row in rowsToClear)
        {
            for(int x = 0; x < _x; x++)
            {
                var tmp = _gameObjects[x, row];
                tmp.SetActive(false);
                _blockPool.Enqueue(tmp);
                _gameObjects[x, row] = null;
            }
        }
		
		//move the Grid above the delete rows(high->low) down (low->high)
        foreach (int clear in rowsToClear)
        {
            for (int row = clear; row < _y - 1; row++)
            {
                for (int x = 0; x < _x; x++)
                {
                    GameObject tmp = _gameObjects[x, row+1];
                    _gameObjects[x, row] = tmp;
                    if (!tmp) continue;
                    tmp.transform.position += Vector3.down;
                }
            }
        }
		
		//top row has to be cleared
        for (int x = 0; x < _x; x++)
        {
            _gameObjects[x, _y - 1] = null;
        }
    }
}
