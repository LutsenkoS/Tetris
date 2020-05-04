using UnityEngine;

public class GameController : MonoBehaviour
{
	public GameState State { get; set; }

	[SerializeField] FigureController[] Figures;
	[SerializeField] Transform FiguresContainer;

	private View mainView;
	private GameModel gameModel;
	private InputModel inputModel;
	private FieldController fieldController;

	private int X_field;
	private float timeout, curSpeed;
	private int score { get; set; }

	private void Awake()
	{
		SetReferences();
	}

	void Start()
	{
		mainView.ShowMenu(true);
		State.State = GameStates.Menu;
		X_field = gameModel.FieldWidth / 2;
	}
	void Update()
	{
		Tick();
	}

	public void OnStart()
	{
		mainView.ShowMenu(false);
		mainView.ShowGame(true);
		fieldController = new FieldController(gameModel.FieldWidth, gameModel.FieldHeight, FiguresContainer);
		fieldController.GameOver += OnGameOver;
		fieldController.DeleteLine += OnDeleteLine;
		NewGame();
	}
	public void OnRestart()
	{
		mainView.ShowGameOver(false, score);
		mainView.ShowGame(true);
		NewGame();
	}
	public void MoveSide(Direction side)
	{
		fieldController.MoveSide(side);
	}

	public void Rotation(Direction left)
	{
		fieldController.Rotation(left);
	}
	public void IncreaseSpeed()
	{
		curSpeed = gameModel.IncreasedSpeed;
		curSpeed = Mathf.Clamp(curSpeed, 0.1f, 1f);
	}

	public void ResetSpeed()
	{
		curSpeed = gameModel.Speed;
		curSpeed = Mathf.Clamp(curSpeed, 0.1f, 1f);
	}
	public InputModel GetInputData()
	{
		return inputModel;
	}
	private void SetReferences()
	{
		gameModel = Resources.Load<GameModel>("GameModel");
		inputModel = Resources.Load<InputModel>("InputModel");
		mainView = FindObjectOfType<View>();
		State = new GameState();
	}
	void NewGame()
	{
		score = 0;
		fieldController.ClearField();
		CreateNewFigure();
		State.State = GameStates.InGame;
	}
	private void Tick()
	{
		if (State.State != GameStates.InGame)
			return;

		timeout += Time.deltaTime;
		if (timeout > curSpeed)
		{
			timeout = 0;
			MoveDown();	
		}
	}
	private void OnDeleteLine()
	{
		score++;
		mainView.UpdateScore(score);
	}
	private void OnGameOver()
	{
		State.State = GameStates.GameOver;
		mainView.ShowGame(false);
		mainView.ShowGameOver(true, score);
	}
	private void MoveDown()
	{
		fieldController.MoveDown();
		if (fieldController.IsFigureDown())
			CreateNewFigure();
	}

	void CreateNewFigure()
	{
		int index = Random.Range(0, Figures.Length);
		FigureController figure = Instantiate(Figures[index]);
		figure.SetFigure(new Vector2(X_field, 0), gameModel.CubeColor[index]);
		fieldController.AddNewFigure(figure);
	}

}