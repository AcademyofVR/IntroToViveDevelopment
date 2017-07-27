using UnityEngine;
using UnityEngine.UI;

public class VRIO_WatchExample : VRInteractableObject
{
	public GameObject watchCanvas;
	public Text watchText;

	void Update ()
	{
		//IF watch is showing, update text on watch to display time
		if (watchCanvas.activeInHierarchy)
			watchText.text = System.DateTime.Now.ToString("HH:mm:ss");
	}

	public override void RayEnter(RaycastHit hit, VRControllerInput controller = null)
	{
		//If controller is null, then it was the head that triggered the method
		if (controller == null)
		{
			//Show Watch
			watchCanvas.SetActive(true);
		}
	}

	public override void RayExit(VRControllerInput controller = null)
	{
		//If controller is null, then it was the head that triggered the method
		if (controller == null)
		{
			//Hide Watch
			watchCanvas.SetActive(false);
		}
	}
}
