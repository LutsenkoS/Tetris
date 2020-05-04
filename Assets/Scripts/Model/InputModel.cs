using UnityEngine;
[CreateAssetMenu]
public class InputModel: ScriptableObject, IModel
{
	public KeyCode Right;
	public KeyCode Left;
	public KeyCode RotateLeft;
	public KeyCode RotateRight;
	public KeyCode Down;

}
