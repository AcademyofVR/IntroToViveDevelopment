using UnityEngine;
using Valve.VR;

public class VRIO_Parented : VRInteractableObject
{
	public EVRButtonId pickupButton = EVRButtonId.k_EButton_SteamVR_Trigger;

	public Transform InteractionPoint;
	protected Rigidbody rigidBody;
	protected bool originalKinematicState;
	protected Transform originalParent;

	void Awake()
	{
		rigidBody = GetComponent<Rigidbody>();

		//Capture object's original parent and kinematic state
		originalParent = transform.parent;
		originalKinematicState = rigidBody.isKinematic;
	}

	public override void ButtonPressDown(EVRButtonId button, VRControllerInput controller)
	{
		//If pickup button was pressed
		if (button == pickupButton)
			ParentedPickup(controller);
	}

	public override void ButtonPressUp(EVRButtonId button, VRControllerInput controller)
	{
		//If pickup button was released
		if (button == pickupButton)
			ParentedRelease(controller);
	}

	protected void ParentedPickup(VRControllerInput controller)
	{
		//Make object kinematic
		//(Not effected by physics, but still able to effect other objects with physics)
		rigidBody.isKinematic = true;

		//Parent object to hand
		transform.SetParent(controller.gameObject.transform);

		//If there is an interaction point, snap object to that point
		if (InteractionPoint != null)
		{
			//Set the position of the object to the inverse of the interaction point's local position.
			transform.localPosition = -InteractionPoint.localPosition;

			//Set the local rotation of the object to the inverse of the rotation of the interaction point.
			//When you're setting your interaction point the blue arrow (Z) should be pointing in the direction you want your hand to be pointing
			//and the green arrow (Y) should be pointing "up".
			transform.localRotation = Quaternion.Inverse(InteractionPoint.localRotation);
		}	
	}

	protected void ParentedRelease(VRControllerInput controller)
	{
		//Make sure the hand is still the parent. 
		//Could have been transferred to anothr hand.
		if (transform.parent == controller.gameObject.transform)
		{
			//Return previous kinematic state
			rigidBody.isKinematic = originalKinematicState;

			//Set object's parent to its original parent
			if (originalParent != controller.gameObject.transform)
			{
				//Ensure original parent recorded wasn't somehow the controller (failsafe)
				transform.SetParent(originalParent);
			}
			else
			{
				transform.SetParent(null);
			}

			//Throw object
			ThrowObject(controller);
		}
	}

	protected void ThrowObject(VRControllerInput controller)
	{
		//Set object's velocity and angular velocity to that of controller
		rigidBody.velocity = controller.device.velocity;
		rigidBody.angularVelocity = controller.device.angularVelocity;
	}
}
