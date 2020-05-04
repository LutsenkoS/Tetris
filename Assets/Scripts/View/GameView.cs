using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : BaseView
{
    public Text ScoreText;

    public void UpdateScoreText(int score)
    {
        ScoreText.text = score.ToString();
    }
}
