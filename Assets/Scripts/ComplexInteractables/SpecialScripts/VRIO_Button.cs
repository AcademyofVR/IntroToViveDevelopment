using UnityEngine;
using Valve.VR;

public class VRIO_Button : VRInteractableObject
{
	public Transform button;
	public Vector3 buttonDownPos;
	public float buttonClickSpeed = 10;
	public EVRButtonId buttonToTrigger = EVRButtonId.k_EButton_SteamVR_Trigger;

	protected Vector3 buttonStartPos;
	protected Vector3 currentButtonDestination;

	public delegate void ButtonPress();
	public static event ButtonPress OnButtonPress;

	public void Awake()
	{
		//Get button's "up" position, set it to current and up destination variables
		buttonStartPos = button.localPosition;
		currentButtonDestination = button.localPosition;
	}

	public void Update()
	{
		//Check to see if button is in the same position as its destination position
		if (button.localPosition != currentButtonDestination)
		{
			//If its not, lerp toward it at a predefined speed.
			//Remember to multiply movements in Update by Time.deltaTime, so that things don't move faster on computers
			//with higher framerates
			Vector3 position = Vector3.MoveTowards(button.localPosition, currentButtonDestination, buttonClickSpeed * Time.deltaTime);
			button.localPosition = position;
		}
	}

	public override void ButtonPressDown(EVRButtonId button, VRControllerInput controller)
	{
		//If button is desired "trigger" button
		if (button == buttonToTrigger)
		{
			//Set button's destination position to the "down" position
			currentButtonDestination = buttonDownPos;

			TriggerButtonPress();
		}
	}

	public override void ButtonPressUp(EVRButtonId button, VRControllerInput controller)
	{
		//Set button's destination position to the "up" position
		if (button == buttonToTrigger)
		{
			currentButtonDestination = buttonStartPos;
		}
	}

	protected virtual void TriggerButtonPress()
	{
		//If anything is hooked up to the event, call it
		if (OnButtonPress != null)
			OnButtonPress();
	}
}
