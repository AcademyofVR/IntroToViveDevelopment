using UnityEngine;

public class ResetController : VRInteractableObject
{
	public static ResetController instance;
	
	public GameObject[] objectsToReset;

	protected Vector3[] originalPositions;
	protected Quaternion[] originalRotations;

	void Awake()
	{
		instance = this;

		originalPositions = new Vector3[objectsToReset.Length];
		originalRotations = new Quaternion[objectsToReset.Length];
		for (int i = 0; i < objectsToReset.Length; i++)
		{
			originalPositions[i] = objectsToReset[i].transform.position;
			originalRotations[i] = objectsToReset[i].transform.rotation;
		}

		VRControllerInput.OnTouchpadPress += ResetScene;
	}

	public void ResetScene()
	{
		for (int i = 0; i < objectsToReset.Length; i++)
		{
			objectsToReset[i].transform.position = originalPositions[i];
			objectsToReset[i].transform.rotation = originalRotations[i];

			Rigidbody rb = objectsToReset[i].GetComponent<Rigidbody>();
			if (rb)
			{
				rb.velocity = Vector3.zero;
			}
		}
	}
}
