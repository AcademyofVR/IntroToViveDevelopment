using UnityEngine;

public class SimpleExample_VRInteractableObject : VRInteractableObject
{
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

	public void Pickup(SimpleExample_VRControllerInput controller)
	{
		//Make object kinematic
		//(Not effected by physics, but still able to effect other objects with physics)
		rigidBody.isKinematic = true;

		//Parent object to hand
		transform.SetParent(controller.gameObject.transform);
	}

	public void Release(SimpleExample_VRControllerInput controller)
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
			rigidBody.velocity = controller.device.velocity;
			rigidBody.angularVelocity = controller.device.angularVelocity;
		}
	}
}
