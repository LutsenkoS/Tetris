using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : BaseView
{
    public Button StartButton;

    public event ButtonEvent StartClick;
    private void Start()
    {
        StartButton.onClick.AddListener(OnStartBtnClick);
    }

    private void OnStartBtnClick()
    {
        StartClick?.Invoke();
    }
}
