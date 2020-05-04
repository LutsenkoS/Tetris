using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseView : MonoBehaviour
{
    public delegate void ButtonEvent();

    public GameObject Panel;

    public virtual void Show(bool show)
    {
        Panel.SetActive(show);
    }

}
