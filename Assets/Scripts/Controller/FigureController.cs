using System;
using UnityEngine;

public class FigureController: MonoBehaviour, IFigure
{
    public Cube[] cubes;
    
    public void MoveDown() 
	{
		transform.position += Vector3.down;
	}
	public void MoveSide(int direction) 
	{
		transform.position += new Vector3(direction, 0, 0);
	}

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }

    public void DestroyFigure()
    {
        Destroy(gameObject);
    }

    public void SetFigure(Vector2 position, Color color)
    {
        SetPosition(position);
        SetColor(color);
    }

    private void SetColor(Color color)
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].SetColor(color);
        }
    }

    private void SetPosition(Vector2 position)
    {
        transform.position = position;
    }
    

}