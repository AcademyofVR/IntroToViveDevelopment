using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using System.Collections.Generic;

public class SimpleExample_VRControllerInput : MonoBehaviour
{
	//Should only ever be one, but just in case
	protected List<SimpleExample_VRInteractableObject> heldObjects;

	//Controller References
	protected SteamVR_TrackedObject trackedObj;
	public SteamVR_Controller.Device device
	{
		get
		{
			return SteamVR_Controller.Input((int)trackedObj.index);
		}
	}

	void Awake()
	{
		//Instantiate lists
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		heldObjects = new List<SimpleExample_VRInteractableObject>();
	}

	void Update()
	{
		if (heldObjects.Count > 0)
		{
			//If trigger is releasee
			if (device.GetPressUp(EVRButtonId.k_EButton_SteamVR_Trigger))
			{
				//Release any held objects
				for (int i = 0; i < heldObjects.Count; i++)
				{
					heldObjects[i].Release(this);
				}
				heldObjects = new List<SimpleExample_VRInteractableObject>();
			}
		}
	}

	void OnTriggerStay(Collider collider)
	{
		//If object is an interactable item
		SimpleExample_VRInteractableObject interactable = collider.GetComponent<SimpleExample_VRInteractableObject>();
		if (interactable != null)
		{
			//If trigger button is down
			if (device.GetPressDown(EVRButtonId.k_EButton_SteamVR_Trigger))
			{
				//Pick up object
				interactable.Pickup(this);
				heldObjects.Add(interactable);
			}
		}
	}
}
