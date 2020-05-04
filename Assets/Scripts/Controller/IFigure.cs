using UnityEngine;

public interface IFigure
{
    void SetFigure(Vector2 position, Color color);
    void MoveDown();
    void MoveSide(int direction);
    void SetParent(Transform parent);
    void DestroyFigure();
}