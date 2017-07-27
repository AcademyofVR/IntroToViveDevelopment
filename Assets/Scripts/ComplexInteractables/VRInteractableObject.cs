using UnityEngine;
using Valve.VR;

public class VRInteractableObject : MonoBehaviour
{
	/// <summary>
	/// Called when controller passes into trigger
	/// </summary>
	/// <param name="controller"></param>
	public virtual void TriggerEnter(VRControllerInput controller)
	{
		//Empty. Overriden meothod only.
	}

	/// <summary>
	/// Called when controller passes out of trigger
	/// </summary>
	/// <param name="controller"></param>
	public virtual void TriggerExit(VRControllerInput controller)
	{
		//Empty. Overriden meothod only.
	}

	/// <summary>
	/// Called when button is pressed down while controller is inside object
	/// </summary>
	/// <param name="controller"></param>
	public virtual void ButtonPressDown(EVRButtonId button, VRControllerInput controller)
	{
		//Empty. Overriden meothod only.
	}

	/// <summary>
	/// Called when button is released after an object has been "grabbed".
	/// </summary>
	/// <param name="controller"></param>
	public virtual void ButtonPressUp(EVRButtonId button, VRControllerInput controller)
	{
		//Empty. Overriden meothod only.
	}

	/// <summary>
	/// Called when either the head or a controller is pointed at an object
	/// </summary>
	/// <param name="controller">Leave null if ray is coming from head</param>
	public virtual void RayEnter(RaycastHit hit, VRControllerInput controller = null)
	{
		//Empty. Overriden meothod only.
	}

	/// <summary>
	/// Called every frame the head or a controller is pointed at an object
	/// </summary>
	/// <param name="controller">Leave null if ray is coming from head</param>
	public virtual void RayStay(RaycastHit hit, VRControllerInput controller = null)
	{
		//Empty. Overriden meothod only.
	}

	/// <summary>
	/// Called when either the head or a controller leaves the object
	/// </summary>
	/// <param name="controller">Leave null if ray is coming from head</param>
	public virtual void RayExit(VRControllerInput controller = null)
	{
		//Empty. Overriden meothod only.
	}
}