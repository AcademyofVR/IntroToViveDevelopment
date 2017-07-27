using UnityEngine;

public class VRIO_RaycastCanvasExample : VRInteractableObject
{
	public RectTransform headIndicatorRect;
	public RectTransform[] handIndicatorRects;
	public RectTransform canvasRect;

	protected int leftControllerIndex
	{
		get
		{
			//Get index of leftmost controller
			return SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
		}
	}

	public override void RayStay(RaycastHit hit, VRControllerInput controller = null)
	{
		//Get position of the raycast hit, relative to the Canvas
		Vector2 position = canvasRect.InverseTransformPoint(hit.point);

		//If there is a controller script, which meands that a controller is triggering
		if (controller)
		{
			//Get a controller index of either 0 or 1, 0 if the controller is leftmost, 1 otherwise
			int controllerIndex = controller.device.index == leftControllerIndex ? 0 : 1;

			//Make sure that enough objects on the canvas are assigned
			if (controllerIndex < handIndicatorRects.Length)
			{
				//Move canvas object that matches the hands
				handIndicatorRects[controllerIndex].anchoredPosition = position;
			}
		}
		else
		{
			//Move head canvas object to match position
			headIndicatorRect.anchoredPosition = position;
		}
	}
}
