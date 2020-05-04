using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController 
{
	public FigureState State { get; set; }

	public delegate void GameEvent();
	public event GameEvent GameOver;
	public event GameEvent DeleteLine;	

	int fieldWidth;
	int fieldHeight;
	Cube[,] field;
	FigureController currentFigure;
	Transform container;
	List<Cube> storedCubes;
    public FieldController(int _fieldWidth, int _fieldHeight, Transform _container)
    {
		fieldWidth = _fieldWidth;
		fieldHeight = _fieldHeight;
		container = _container;
		storedCubes = new List<Cube>();
		State = new FigureState();
    }
	public void MoveDown()
	{
		if (!CanMoveDown())
		{
			AddToField();
		}
		else
		{
			currentFigure.MoveDown();
		}
	}
	
	public void MoveSide(Direction direction)
	{
		int index = direction == Direction.Left ? 1 : -1;

		if (CanMoveSide(index))
		{
			currentFigure.MoveSide(index);
		}
	}
	public bool IsGameOver()
	{
		for (int i = 0; i < currentFigure.cubes.Length; i++)
		{
			Transform tr = currentFigure.cubes[i].transform;
			int x = Mathf.RoundToInt(tr.position.x);
			int y = Mathf.Abs(Mathf.RoundToInt(tr.position.y));

			if (y < fieldHeight - 1)
			{
				if (field[x, y + 1])
				{
					return true;
				}
			}
		}

		return false;
	}
	public void ClearField()
	{
		if (storedCubes.Count > 0)
			for (int i = 0; i < storedCubes.Count; i++)
			{
				storedCubes[i].Destroy();
			}
		currentFigure?.DestroyFigure();
		field = new Cube[fieldWidth, fieldHeight];
	}

	public bool IsFigureDown()
	{
		if (State.State == FigureStates.IsDown)
			return true;
		return false;
	}

	public void AddToField()
	{
		storedCubes.RemoveAll(t => t == null);
		for (int i = 0; i < currentFigure.cubes.Length; i++)
		{
			Cube cube = currentFigure.cubes[i];
			int x = (int)cube.GetPosition().x;
			int y = (int)cube.GetPosition().y;
			field[x, y] = cube;
			storedCubes.Add(cube);
			cube.SetParent(container);
		}
		State.State = FigureStates.IsDown;
		currentFigure.DestroyFigure();
		FieldUpdate();
	}

	public void AddNewFigure(FigureController figure)
	{
		currentFigure = figure;
		if (IsGameOver())
		{
			GameOver?.Invoke();
		}
		else
			State.State = FigureStates.Moving;

	}
	private bool CanMoveDown()
	{
		for (int i = 0; i < currentFigure.cubes.Length; i++)
		{
			int x = (int)currentFigure.cubes[i].GetPosition().x;
			int y = (int)currentFigure.cubes[i].GetPosition().y;

			if (y < fieldHeight - 1)
			{
				if (field[x, y + 1])
				{
					return false; 
				}
			}
			else
			{
				return false;
			}
		}
		return true;
	}
	private bool CanMoveSide(int direction)
	{
		for (int i = 0; i < currentFigure.cubes.Length; i++)
		{
			int x = (int)currentFigure.cubes[i].GetPosition().x;
			int y = (int)currentFigure.cubes[i].GetPosition().y;

			if (x < fieldWidth - 1 && direction > 0)
			{
				if (field[x + 1, y])
				{
					return false;
				}
			}
			else if (x > 0 && direction < 0)
			{
				if (field[x - 1, y])
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		return true;
	}
	
	private bool InsideField() 
	{
		for (int i = 0; i < currentFigure.cubes.Length; i++)
		{
			int x = (int)currentFigure.cubes[i].GetPosition().x;
			int y = (int)currentFigure.cubes[i].GetPosition().y;

			if (x < 0 || x > fieldWidth - 1 || y > fieldHeight - 1 || y < 0)
			{
				return false;
			}
		}
		return true;
	}

	private bool CheckOverlap()
	{
		for (int i = 0; i < currentFigure.cubes.Length; i++)
		{
			int x = (int)currentFigure.cubes[i].GetPosition().x;
			int y = (int)currentFigure.cubes[i].GetPosition().y;

			if (field[x, y])
			{
				return true;
			}
		}
		return false;
	}

	bool ShiftShape(int iter, int shift) 
	{
		currentFigure.MoveSide(shift); 
		if (InsideField()) 
		{
			if (CheckOverlap())
			{
				currentFigure.MoveSide(-shift); 
				return true;
			}
			return false;
		}

		currentFigure.MoveSide(-shift * 2);
		if (InsideField())
		{
			if (CheckOverlap())
			{
				currentFigure.MoveSide(shift);
				return true;
			}
			return false;
		}
		currentFigure.MoveSide(shift);

		if (iter > 0)
		{
			return ShiftShape(iter - 1, shift + 1);
		}
		else
		{
			return true;
		}
	}

	public void Rotation(Direction direction)
	{
		int angle = direction == Direction.Left ? -90 : 90;
		bool result = false;
		
		currentFigure.transform.Rotate(0, 0, angle);

		if (!InsideField())
		{
			result = ShiftShape(2, 1);
		}
		else
		{
			result = CheckOverlap();
		}

		if (result)
		{
			currentFigure.transform.Rotate(0, 0, -angle);
		}
	}

	private void FieldUpdate()
	{
		int line = -1;
		line = CheckLine();
		while (line != -1)
		{
			DestroyLine(line);
			ShiftLine(line);
			line = CheckLine();
		}
	}

	private void ShiftLine(int line) 
	{
		for (int x = 0; x < fieldWidth; x++)
		{
			for (int y = line; y > 0; y--)
			{
				field[x, y] = field[x, y - 1];
			}
		}

		for (int i = 0; i < fieldWidth; i++)
		{
			field[i, 0] = null;
		}

		foreach (Cube cube in storedCubes)
		{
			int y = (int)cube.GetPosition().y;
			if (y < line)
			{
				cube.UpdatePosition(Vector3.down);
			}
		}
	}

	private void DestroyLine(int line) 
	{	
		foreach(var cube in storedCubes)
		{
			int x = (int)cube.GetPosition().x;
			int y = (int)cube.GetPosition().y;
			if (y == line)
			{
				field[x, y] = null;
				cube.Destroy();
			}
		}
		DeleteLine?.Invoke();
	}

	private int CheckLine()
	{
		int i = 0;
		for (int y = 0; y < fieldHeight; y++)
		{
			for (int x = 0; x < fieldWidth; x++)
			{
				if (field[x, y])
					i++;
			}
			if (i == fieldWidth)
			{
				return y;
			}
			i = 0;
		}
		return -1;
	}
}
