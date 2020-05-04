using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
   	[SerializeField] MenuView menuView;
	[SerializeField] GameView gameView;
	[SerializeField] GameOverView gameOverView;

	GameController gameController;

	private void Awake()
	{
		SetReferences();
	}
	
	private void Start()
	{
		SetEventHandlers();
	}
	
	void Update()
	{
		if (Input.GetKeyDown(gameController.GetInputData().Right))
		{
			gameController.MoveSide(Direction.Right);
		}
		else if (Input.GetKeyDown(gameController.GetInputData().Left))
		{
			gameController.MoveSide(Direction.Left);
		}
		else if (Input.GetKeyDown(gameController.GetInputData().RotateLeft))
		{
			gameController.Rotation(Direction.Left);
		}
		else if (Input.GetKeyDown(gameController.GetInputData().RotateRight))
		{
			gameController.Rotation(Direction.Right);
		}
		else if (Input.GetKey(gameController.GetInputData().Down))
		{
			gameController.IncreaseSpeed();
		}
		else
		{
			gameController.ResetSpeed();
		}
	}
	public void ShowMenu(bool show)
	{
		menuView.Show(show);
	}
	public void ShowGame(bool show)
	{
		gameView.Show(show);
	}
	public void ShowGameOver(bool show, int score)
	{
		gameOverView.Show(show);
		gameOverView.UpdateScore(score);
	}
	public void UpdateScore(int score)
	{
		gameView.UpdateScoreText(score);
	}
	private void SetReferences()
	{
		gameController = FindObjectOfType<GameController>();
	}
	private void SetEventHandlers()
	{
		menuView.StartClick += OnStart;
		gameOverView.RestartClick += OnRestart;
	}
	
	private void OnRestart()
	{
		gameController.OnRestart();
	}

	private void OnStart()
	{
		gameController.OnStart();
	}

}
