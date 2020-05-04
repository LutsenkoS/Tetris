using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : BaseView
{
    public Button RestartButton;
    public Text ScoreText;

    public event ButtonEvent RestartClick;
    private void Start()
    {
        RestartButton.onClick.AddListener(OnRestartBtnClick);
    }
    public void UpdateScore(int score)
    {
        ScoreText.text = score.ToString();
    }
    private void OnRestartBtnClick()
    {
        RestartClick?.Invoke();
    }
}
