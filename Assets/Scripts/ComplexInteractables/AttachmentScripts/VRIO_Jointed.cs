using UnityEngine;
using Valve.VR;

public class VRIO_Jointed : VRInteractableObject
{
	public EVRButtonId pickupButton = EVRButtonId.k_EButton_SteamVR_Trigger;
	public bool useBreakForce = false;

	protected Rigidbody rigidBody;
	protected FixedJoint fixedJoint;

	void Awake()
	{
		rigidBody = GetComponent<Rigidbody>();
	}

	public override void ButtonPressDown(EVRButtonId button, VRControllerInput controller)
	{
		if (button == pickupButton)
			JointedPickup(controller);
	}

	public override void ButtonPressUp(EVRButtonId button, VRControllerInput controller)
	{
		if (button == pickupButton)
			JointedRelease(controller);
	}

	protected void JointedPickup(VRControllerInput controller)
	{
		//Check if it already has a joint (held by other hand)
		if (fixedJoint == null)
		{
			//Add joint to object
			fixedJoint = gameObject.AddComponent<FixedJoint>();

			//You don't have to set a break force, but if you don't, 
			//when you drag your object behind immovable things, they'll just weirdly be
			//stuck behind them until you let go.
			//By setting a break force, you "drop" the item under too much strain.
			if (useBreakForce)
			{
				fixedJoint.breakForce = 500;
			}
		}

		//Attach to controller
		fixedJoint.connectedBody = controller.GetComponent<Rigidbody>();
	}

	protected void JointedRelease(VRControllerInput controller)
	{
		//Make sure joint isnt null
		if (fixedJoint != null)
		{
			//If object is still jointed to hand (could have been taken by other hand)
			if (fixedJoint.connectedBody == controller.GetComponent<Rigidbody>())
			{
				//Remove joint
				Destroy(fixedJoint);
				fixedJoint = null;
			}

			ThrowObject(controller);
		}
	}

	void OnJointBreak(float breakForce)
	{
		Debug.Log("Joint broke with " + breakForce);
		Destroy(fixedJoint);
		fixedJoint = null;
	}

	protected void ThrowObject(VRControllerInput controller)
	{
		//Set object's velocity and angular velocity to that of controller
		rigidBody.velocity = controller.device.velocity;
		rigidBody.angularVelocity = controller.device.angularVelocity;
	}
}
