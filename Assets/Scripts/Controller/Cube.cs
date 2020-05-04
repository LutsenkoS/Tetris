using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Renderer Renderer;
    public void UpdatePosition(Vector3 position)
    {
        transform.position += position;
    }
    public Vector2 GetPosition()
    {
        int x = Mathf.RoundToInt(transform.position.x);
        int y = Mathf.Abs(Mathf.RoundToInt(transform.position.y));
        Vector2 result = new Vector2(x, y);
        return result;
    }
    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void SetColor(Color color)
    {
        Renderer.material.color = color;
    }
}
