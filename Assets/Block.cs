using UnityEngine;

public class Block : MonoBehaviour
{

    public ListVariable _Grid;

    public bool CheckValidMove(Vector2 newPosition)
    {
        Vector2 toCheck =  newPosition;
        if (_Grid.Get((int)toCheck.x, (int)toCheck.y))
        {
            return true;
        }
        return false;
    }


    /*
     * Rotates each Block around the center block(0,0)
     */
    public Vector2 Rotate(int direction = 1)
    {
        Vector2 swap = transform.localPosition;
        
        //specialcase for stairs
        if (swap.y != 0 && swap.x != 0)
        {
            if (swap.x != swap.y)
            {
                direction = -direction;
            }
            swap.x *= direction;
            swap.y *= -direction;
            return swap;
        }
        if (swap.x == 0)
        {
            swap.y *= direction;
        }
        else
        {
            swap.x *= -direction;
        }
        float tmp = swap.x;
        swap.x = swap.y;
        swap.y = tmp;

        return swap;

    }
}
