using UnityEngine;
using Valve.VR;

public class VRIO_Physics : VRInteractableObject
{
	public EVRButtonId pickupButton = EVRButtonId.k_EButton_SteamVR_Trigger;

	protected Rigidbody rigidBody;
	protected Transform pickupTransform;
	protected VRControllerInput holdingHand;

	void Awake()
	{
		rigidBody = GetComponent<Rigidbody>();

		//If you don't set the max anglar velocity a bit higher
		//For the physics objects, they wont rotate back into
		//Place very quickly
		rigidBody.maxAngularVelocity = 100;
	}

	void FixedUpdate()
	{
		if (holdingHand != null)
		{
			PhysicsUpdate();
		}
	}

	public override void ButtonPressDown(EVRButtonId button, VRControllerInput controller)
	{
		//If pickup button was pressed
		if (button == pickupButton)
			PhysicsPickup(controller);
	}

	public override void ButtonPressUp(EVRButtonId button, VRControllerInput controller)
	{
		//If pickup button was released
		if (button == pickupButton)
			PhysicsRelease(controller);
	}

	protected void PhysicsPickup(VRControllerInput controller)
	{
		pickupTransform = new GameObject(string.Format("[{0}] PickupTransform", gameObject.name)).transform;
		pickupTransform.parent = controller.transform;
		pickupTransform.position = transform.position;
		pickupTransform.rotation = transform.rotation;

		holdingHand = controller;
	}

	protected void PhysicsUpdate()
	{
		Quaternion rotationDelta = pickupTransform.rotation * Quaternion.Inverse(rigidBody.transform.rotation);
		Vector3 positionDelta = (pickupTransform.position - rigidBody.transform.position);
		float deltaPoses = Time.fixedDeltaTime;

		float angle;
		Vector3 axis;
		rotationDelta.ToAngleAxis(out angle, out axis);

		if (angle > 180)
			angle -= 360;

		if (angle != 0)
		{
			Vector3 AngularTarget = angle * axis;
			rigidBody.angularVelocity = Vector3.MoveTowards(rigidBody.angularVelocity, AngularTarget, 10f * (deltaPoses * 1000));
		}

		Vector3 VelocityTarget = positionDelta / deltaPoses;
		rigidBody.velocity = Vector3.MoveTowards(rigidBody.velocity, VelocityTarget, 10f);
	}

	protected void PhysicsRelease(VRControllerInput controller)
	{
		//Make sure the releasing hand is the hand that's still holding the cube
		if (holdingHand == controller)
		{
			holdingHand = null;
			Destroy(pickupTransform.gameObject);
			pickupTransform = null;
		}
	}
}
