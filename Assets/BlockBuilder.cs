using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject _Prefab;
    [SerializeField]
    private ObjectQueue _blockPool;

    private Vector2[,] _forms = 
    { 
        {new Vector2(0,0),  new Vector2(0,1), new Vector2(0,2), new Vector2(0,-1)},// I
        {new Vector2(0,0),  new Vector2(0,1), new Vector2(1,-1), new Vector2(0,-1)},// L
        {new Vector2(0,0),  new Vector2(0,1), new Vector2(-1,-1), new Vector2(0,-1)},// J
        {new Vector2(0,0),  new Vector2(0,1), new Vector2(1,0), new Vector2(1,1)},// O
        {new Vector2(0,0),  new Vector2(0,1), new Vector2(1,0), new Vector2(0,-1)},// T
        {new Vector2(0,0),  new Vector2(0,1), new Vector2(-1,0), new Vector2(1,1)},// S
        {new Vector2(0,0),  new Vector2(0,1), new Vector2(-1,1), new Vector2(1,0)},// Z
    };

    private Color[] _colors =
    {
        Color.white,
        Color.yellow,
        Color.green,
        Color.red,
        Color.blue,
        Color.black,
        Color.gray
    };


    public void OnEnable()
    {
        GenerateNBLocks(100);
    }

    public List<Block> GetNewForm(Transform parent)
    {
        List<Block> blocks = DraftNewBlocks();
        int x = Random.Range(0, 7);
        for(int i = 0; i<4; i++)
        {
            blocks[i].transform.parent= parent;
            blocks[i].transform.localPosition = _forms[x,i];
            blocks[i].GetComponent<SpriteRenderer>().color = _colors[x];
            blocks[i].gameObject.SetActive(true);
        }
        return blocks;
    }

    private List<Block> DraftNewBlocks()
    {
        List<Block> blocks = new();
        for (int i = 0; i<4; i++)
        {
            GameObject tmp;
            try
            {
                tmp = _blockPool.Dequeue();
            }
            catch
            {
                GenerateNBLocks(20);
                tmp = _blockPool.Dequeue();
            }
            blocks.Add(tmp.GetComponent<Block>());
        }
        
        return blocks;
    }

    private void GenerateNBLocks(int n)
    {
        for(int i = 0; i<n; i++)
        {
            GameObject tmp = Instantiate(_Prefab);
            tmp.SetActive(false);
            _blockPool.Enqueue(tmp);
        }
    }
}
