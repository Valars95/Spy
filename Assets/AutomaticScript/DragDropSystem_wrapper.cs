using UnityEngine;
using FYFY;

[ExecuteInEditMode]
public class DragDropSystem_wrapper : MonoBehaviour
{
	private void Start()
	{
		this.hideFlags = HideFlags.HideInInspector; // Hide this component in Inspector
	}

	public void resetScript()
	{
		MainLoop.callAppropriateSystemMethod ("DragDropSystem", "resetScript", null);
	}

}