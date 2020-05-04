using UnityEngine;
using UnityEditor;
[CreateAssetMenu]
public class GameModel : ScriptableObject, IModel
{
	public float Speed;
	public float IncreasedSpeed;
	public Color[] CubeColor;
	public int FieldWidth;
	public int FieldHeight;

}