using UnityEngine;
using FYFY;

[ExecuteInEditMode]
public class CameraSystem_wrapper : MonoBehaviour
{
	private void Start()
	{
		this.hideFlags = HideFlags.HideInInspector; // Hide this component in Inspector
	}

	public void setCameraOnGO(UnityEngine.GameObject go)
	{
		MainLoop.callAppropriateSystemMethod ("CameraSystem", "setCameraOnGO", go);
	}

}