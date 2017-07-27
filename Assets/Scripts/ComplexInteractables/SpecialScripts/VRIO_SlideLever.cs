using UnityEngine;
using Valve.VR;

public class VRIO_SlideLever : VRInteractableObject
{
	public EVRButtonId buttonToTrigger = EVRButtonId.k_EButton_SteamVR_Trigger;

	[Header("Lever")]
	public Transform lever;
	public float minZ;
	public float maxZ;
	public float[] anchorPoints;
	public float snapDistance = 0.05f;

	protected Transform controllerTransform;

	public void Update()
	{
		//If the user is "grabbing" the lever
		if (controllerTransform != null)
		{
			//Get the controller's position relative to the lever (lever's local position)
			float zPos = transform.InverseTransformPoint(controllerTransform.position).z;

			//Get the lever's current local position
			Vector3 position = lever.transform.localPosition;

			//Set lever's z position to the Z of the converted controller position
			//Clamp it so the lever doesn't go too far either way
			position.z = Mathf.Clamp(zPos, minZ, maxZ);

			//Set lever to new position
			lever.transform.localPosition = position;
		}
	}

	public override void ButtonPressDown(EVRButtonId button, VRControllerInput controller)
	{
		//If button pressed is desired "trigger" button
		if (button == buttonToTrigger)
		{
			controllerTransform = controller.gameObject.transform;
		}
	}

	public override void ButtonPressUp(EVRButtonId button, VRControllerInput controller)
	{
		//If button pressed is desired "trigger" button
		if (button == buttonToTrigger)
		{
			controllerTransform = null;

			//Attempt to snap lever into a slot
			SnapToPosition();
		}
	}

	public delegate void SlideLeverEvent(int position);
	public static event SlideLeverEvent OnLeverSnap;

	protected void SnapToPosition()
	{
		//Cycle through each predefined anchor point
		for (int i = 0; i < anchorPoints.Length; i++)
		{
			//If lever is within "snapping distance" of that anchor point
			if (Mathf.Abs(lever.localPosition.z - anchorPoints[i]) < snapDistance)
			{
				//Get current lever position and update z pos to anchor point
				Vector3 position = lever.transform.localPosition;
				position.z = anchorPoints[i];
				lever.transform.localPosition = position;

				//Call lever snap event
				if (OnLeverSnap != null)
					OnLeverSnap(i);
				
				//Break so it stops checking other anchor points
				break;
			}
		}
	}
}
